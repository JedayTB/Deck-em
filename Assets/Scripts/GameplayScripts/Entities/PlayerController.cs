using System.Collections.Generic;
using UnityEngine;

public delegate void CardAction();
public class PlayerController : A_Entity
{
  [Header("Player Specifics")]
  [SerializeField] Camera cam;
  public Vector3 camForward { get { return cam.transform.forward; } }
  [SerializeField] float pitchAngle;
  [SerializeField] float yawAngle;

  [SerializeField] float cameraYawSensitivity = 5f;
  [SerializeField] float cameraPitchSensitivity = 5f;

  [SerializeField] float maxPitchAngle = 70f;
  [SerializeField] KeyCode JumpKey = KeyCode.Space;
  [Header("Card Settings")]
  Stack<A_Card> currentCards;
  public Stack<A_Card> PlayerCards { get { return currentCards; } }
  protected override void Init()
  {
    base.Init();
    cam = Camera.main;
    Cursormanager.defaultCursorLockedInPlace();

    currentCards = new();
  }
  void Start()
  {
    Init();
  }
  void Update()
  {
    handleInput();
    rotateBodyY();
  }
  public void PickedUpCard(A_Card card)
  {
    currentCards.Push(card);
  }
  protected override void rotateBodyY()
  {
    Vector3 eul = cam.transform.rotation.eulerAngles;
    eul.x = 0f;
    eul.z = 0f;
    entityBody.transform.rotation = Quaternion.Euler(eul);
  }
  void handleInput()
  {
    //Movement 
    float hAxis = Input.GetAxisRaw("Horizontal");
    float vAxis = Input.GetAxisRaw("Vertical");

    // Camera
    yawAngle += Input.GetAxisRaw("Mouse X") * cameraYawSensitivity * Time.deltaTime;
    pitchAngle -= Input.GetAxisRaw("Mouse Y") * cameraPitchSensitivity * Time.deltaTime;

    pitchAngle = Mathf.Clamp(pitchAngle, -maxPitchAngle, maxPitchAngle);

    Vector3 camEulAnlges = cam.transform.rotation.eulerAngles;
    camEulAnlges.x = pitchAngle;
    camEulAnlges.y = yawAngle;

    cam.transform.rotation = Quaternion.Euler(camEulAnlges);

    moveDirection = vAxis * moveSpeed * cam.transform.forward;
    moveDirection += hAxis * moveSpeed * cam.transform.right;
    moveDirection.Normalize();

    if (Input.GetKeyDown(JumpKey) && isGrounded)
    {
      print("Jump");
      Jump();
    }
    //Card stuff
    if (Input.GetMouseButtonDown(1))
    {
      useCard();
    }
  }
  void useCard()
  {
    try
    {
      if (currentCards.Peek() != null)
      {
        var c = currentCards.Pop();
        c.onCardUsed?.Invoke();
        Destroy(c.gameObject);
      }
    }
    catch (System.Exception e)
    {
      string msg = e.Message;
      print("Player card stack empty");
      
    }

  }

  void FixedUpdate()
  {
    CheckGrounded();
    ChangeRBDrag();
    AccelerateRB();
  }
}

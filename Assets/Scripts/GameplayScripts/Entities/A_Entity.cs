using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class A_Entity : MonoBehaviour
{
  [Header("Component References")]
  [SerializeField] protected Rigidbody rb;
  public Rigidbody RigidBody { get { return rb; } }
  [SerializeField] protected GameObject entityBody;

  [Header("Movement Variables")]
  [SerializeField] protected float moveSpeed = 5f;
  [SerializeField] protected float accelerateDrag = 1.5f;
  [SerializeField] protected float stoppingDrag = 5f;
  [SerializeField] protected float airborneDrag = 0.5f;
  [SerializeField] protected float maxSpeed = 10f;
  [Space(15)]
  [SerializeField] protected float jumpForce = 200f;

  [Header("Raycast information")]
  [SerializeField] protected LayerMask groundLayers;
  [SerializeField] protected float boxCastBounds = 0.5f;
  [SerializeField] protected float raycastLength = 0.5f;
  protected RaycastHit boxcastHit;
  protected bool isGrounded = true;
  [SerializeField] protected Vector3 moveDirection;

  // Start is called before the first frame update
  protected virtual void Init()
  {
    rb = GetComponent<Rigidbody>();
    rb.freezeRotation = true;
  }
  protected virtual void rotateBodyY()
  {
    Debug.Log("Support in children.");
  }
  protected virtual void CheckGrounded()
  {
    Vector3 bounds = new(boxCastBounds, boxCastBounds, boxCastBounds);
    isGrounded = Physics.BoxCast(transform.position, bounds, Vector3.down, out boxcastHit, Quaternion.identity, raycastLength, groundLayers);
    Debug.DrawRay(transform.position, Vector3.down * raycastLength, isGrounded == true ? Color.green : Color.red);
  }
  protected virtual void ChangeRBDrag()
  {
    if (moveDirection == Vector3.zero && isGrounded)
    {
      rb.drag = stoppingDrag;
    }
    else if (moveDirection != Vector3.zero && isGrounded)
    {
      rb.drag = accelerateDrag;
    }
    else
    {
      rb.drag = airborneDrag;
    }
  }
  protected virtual void Jump()
  {
    RigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
  }
  protected virtual void AccelerateRB()
  {
    // If modeDirection isn't normalized yet, do it again
    Vector3 moveForce = moveSpeed * moveDirection.normalized;

    rb.AddForce(moveForce);

    Vector3 vel = rb.velocity;

    vel.x = Mathf.Clamp(vel.x, -maxSpeed, maxSpeed);
    vel.y = Mathf.Clamp(vel.y, -maxSpeed, maxSpeed);
    vel.z = Mathf.Clamp(vel.z, -maxSpeed, maxSpeed);
  }
  void OnDrawGizmos()
  {
  }
}

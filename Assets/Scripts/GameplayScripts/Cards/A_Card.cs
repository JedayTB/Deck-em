using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public abstract class A_Card : MonoBehaviour
{
  public CardAction onCardUsed;
  protected PlayerController player;
  protected BoxCollider boxColl;
  [SerializeField] protected Renderer rend;
  protected virtual void Init()
  {
    boxColl = GetComponent<BoxCollider>();
    boxColl.isTrigger = true;
  }
  void Start()
  {
    Init();
  }
  protected virtual void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.transform.parent.CompareTag("Player"))
    {
      player = other.gameObject.transform.parent.GetComponent<PlayerController>();
      player.PlayerCards.Push(this);

      rend.enabled = false;
      boxColl.enabled = false;

      Debug.Log("picked up by player");
    }
  }
}

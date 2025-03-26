using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ThrowingCard : MonoBehaviour
{
  public float throwSpeed = 20f;
  public float lifeTime = 3f;
  void Awake()
  {
    // Incase I forgot to set as trigger
    var bc = GetComponent<BoxCollider>();
    bc.isTrigger = true;
    Destroy(this.gameObject, lifeTime);
  }
  void Update()
  {
    transform.position += throwSpeed * Time.deltaTime * transform.forward;
  }

  void OnTriggerEnter(Collider other)
  {
    print($"triggered {other.name}");
    if (other.gameObject.CompareTag("Player"))
    {
      print("hit player");
    }
  }
}

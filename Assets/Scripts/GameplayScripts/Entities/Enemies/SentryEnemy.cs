using System.Collections;
using UnityEngine;

public class SentryEnemy : A_Entity
{
  public bool combatEnabled = false;
  [Header("Object references")]
  PlayerController player;
  [SerializeField] private ThrowingCard ThrowingCard;
  [SerializeField] private Transform enemySentryCannon;
  [Header("Projectile Settings")]
  [SerializeField] private float shotCoolDown = 1.5f;
  [SerializeField] private float projectileSpeed = 10f;

  protected override void Init()
  {
    base.Init();
    player = GameStateManager.Instance.player;
    combatEnabled = true;
    StartCoroutine(updateTick());
  }
  void Start()
  {
    Init();
  }
  IEnumerator updateTick()
  {
    var st = StartCoroutine(shootingStuff());
    while (true)
    {
      cannonLookat();
      yield return null;
    }
  }
  IEnumerator shootingStuff()
  {
    float count = 0f;
    while (true)
    {
      count += Time.deltaTime;
      if (count > shotCoolDown)
      {
        shootCard();
        count = 0f;
      }
      yield return null;
    }
  }
  void shootCard()
  {
    var c = Instantiate(ThrowingCard);
    c.transform.rotation = enemySentryCannon.rotation;
    c.transform.position = enemySentryCannon.transform.position;
    c.throwSpeed = projectileSpeed;
  }
  void cannonLookat()
  {
    enemySentryCannon.LookAt(player.transform);
  }
}

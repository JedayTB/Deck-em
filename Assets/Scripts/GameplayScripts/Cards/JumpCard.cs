using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCard : A_Card
{
  [SerializeField] private float JumpForce = 500f;

  protected override void Init()
  {
    base.Init();
    onCardUsed = Jump;
  }
  void Jump()
  {
    player.RigidBody.AddForce(JumpForce * Vector3.up);
    print("Jump?");
  }
}

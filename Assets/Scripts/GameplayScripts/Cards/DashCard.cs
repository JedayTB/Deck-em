using UnityEngine;

public class DashCard : A_Card
{
  [SerializeField] float dashSpeed = 200f;
  protected override void Init()
  {
    base.Init();
    onCardUsed = dash;
  }
  private void dash()
  {
    player.RigidBody.AddForce(player.camForward * dashSpeed);
    print("Dash?");
  }
}

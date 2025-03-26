using UnityEngine;

public class GameStateManager : MonoBehaviour
{
  private static GameStateManager instance;
  public static GameStateManager Instance { get { return instance; } }

  [Header("Object References")]
  public PlayerController player;

  void InitGame()
  {
    instance = this;

  }
  void Awake()
  {
    InitGame();
  }

  // Update is called once per frame
  void Update()
  {

  }
}

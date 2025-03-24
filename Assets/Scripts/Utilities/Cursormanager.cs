using UnityEngine;

public class Cursormanager : MonoBehaviour
{
  public static void defaultCursorLockedToScreen()
  {
    Cursor.lockState = CursorLockMode.Confined;
    Cursor.visible = true;
  }
  public static void defaultCursorLockedInPlace()
  {
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = true;
  }
}

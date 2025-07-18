using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public void HandleExitGameClick()
    {
        Debug.Log("Exiting from main menu...");
        Application.Quit();
    }
}

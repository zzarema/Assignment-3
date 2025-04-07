using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public void ExitGame()
    {
        UnityEngine.Application.Quit();

        UnityEngine.Debug.Log("Game is exiting...");
    }
}

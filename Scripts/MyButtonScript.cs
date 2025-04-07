using UnityEngine;
using UnityEngine.SceneManagement;

public class MyButtonScript : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene("game");
    }
}


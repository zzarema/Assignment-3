using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOverZone : MonoBehaviour
{
    public GameObject gameOverUI;      
    public float delayBeforeMenu = 2f;  

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ShowGameOver();
        }
    }

    void ShowGameOver()
    {
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }

        
        StartCoroutine(LoadMenuAfterDelay());
    }

    IEnumerator LoadMenuAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeMenu);


        SceneManager.LoadScene("Menu"); 
    }
}

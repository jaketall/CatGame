using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{


    private void Start()
    {
        //Make sure whenever a new game could be started, scores are zeroed
        GameManager.blueCatScore = 0;
        GameManager.yellowCatScore = 0;
        GameManager.whiteCatScore = 0;
        GameManager.greenCatScore = 0;
        GameManager.gameOver = false;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Quitted.");
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}

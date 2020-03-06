using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using InControl;

public class GameManager : MonoBehaviour
{

    public static bool gameOver;
    public static int roundNumber;
    public static bool roundOver;

    public static int blueCatScore;
    public static int yellowCatScore;
    public static int greenCatScore;
    public static int redCatScore;

    public Text roundOverText;
    public Button mainMenuButton;
    public GameObject pause;




    // Start is called before the first frame update
    void Start()
    {
        roundOver = false;
        roundOverText.gameObject.SetActive(false);
        mainMenuButton.gameObject.SetActive(false);
        mainMenuButton.onClick.AddListener(RestartGame);
    }


    // Update is called once per frame
    void Update()
    {
        if (roundOver)
        {
            roundOverText.gameObject.SetActive(true);
            mainMenuButton.gameObject.SetActive(true);
        }
        else
        {
            roundOverText.gameObject.SetActive(false);
            mainMenuButton.gameObject.SetActive(false);
        }

        if (CatIndex.controllersConnected > InputManager.Devices.Count)
        {
            Time.timeScale = 0;
            pause.SetActive(true);
        }
        else
        {
            pause.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public static void EndRound()
    {
        roundOver = true;
        Time.timeScale = 0;
    }

    public static void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

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
    public static bool isPaused;
    public static int level = 1;

    public static int blueCatScore;
    public static int yellowCatScore;
    public static int greenCatScore;
    public static int redCatScore;

    public static float maxScore = 45.0f;

    public Text roundOverText;
    public Button mainMenuButton;
    public GameObject pause;
    public GameObject pauseMenuUI;
    
    // levels
    public GameObject upstairs;
    public List<GameObject> moveWithLevel;
    public Camera cam;
    private ZoomHandler zhScript;
    
    // Start is called before the first frame update
    void Start()
    {
        zhScript = (ZoomHandler) cam.GetComponent<ZoomHandler>();
        if (level == 1)
        {
            upstairs.SetActive(false);
        } else if (level == 2)
        {
            levelTwo();
        }
        roundOver = false;
        isPaused = false;
        pauseMenuUI.gameObject.SetActive(false);
        roundOverText.gameObject.SetActive(false);
        mainMenuButton.gameObject.SetActive(false);
        mainMenuButton.onClick.AddListener(RestartGame);
    }


    // Update is called once per frame
    void Update()
    {
        if(roundOver)
        {
            roundOverText.gameObject.SetActive(true);
            mainMenuButton.gameObject.SetActive(true);
            if (level == 1) {
                level = 2;
            }
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
            if (!GameManager.isPaused)
            {
                pause.SetActive(false);
                Time.timeScale = 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) ||
            InputManager.ActiveDevice.CommandWasPressed)
        {
            if (GameManager.isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void levelTwo()
    {
        for (int i = 0; i < moveWithLevel.Capacity; i++)
        {
            Vector3 pos = moveWithLevel[i].transform.position;
            pos.y = pos.y + 100.8f;
            moveWithLevel[i].transform.position = pos;
        }
        zhScript.minHeight += 150f;
        zhScript.maxHeight += 100f;
        upstairs.SetActive(true);
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

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameManager.isPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameManager.isPaused = true;
    }

}

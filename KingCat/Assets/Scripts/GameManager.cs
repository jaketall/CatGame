using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using InControl;
using UnityEngine.AI;
public class GameManager : MonoBehaviour
{

    public static bool gameOver;
    public static int roundNumber = 1;
    public static bool roundOver;
    public static bool isPaused;
    public static int level = 1;

    public static int blueCatScore;
    public static int yellowCatScore;
    public static int greenCatScore;
    public static int whiteCatScore;

    public static float maxScore = 45;

    public Text roundOverText;
    public GameObject roundNumberText;
    public Button mainMenuButton;
    public GameObject pause;
    public GameObject pauseMenuUI;
    public Button resumeButton;
    
    // levels
    public GameObject upstairs;
    public List<GameObject> moveWithLevel;
    public GameObject dog;
    public Camera cam;
    public GameObject spawner;
    public Vector3 secondFloorSpawnCenter;
    public float startRadius;
    private ZoomHandler zhScript;
    private PowerUpSpawner puSpawnScript;
    // Start is called before the first frame update
    void Start()
    {
        zhScript = (ZoomHandler) cam.GetComponent<ZoomHandler>();
        puSpawnScript = spawner.GetComponent<PowerUpSpawner>();
        if (level == 1)
        {
            levelOne();
        } else if (level == 2)
        {
            levelTwo();
        } else if (level == 3)
        {
            levelOne();
        } else if (level == 4)
        {
            levelTwo();
        }
        else if (level == 5) 
        {
            levelOne();
        }

        level++;
        
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
        roundNumberText.GetComponent<Text>().text = "Round " + roundNumber;
            //TODO: hardcoded white, blue, yellow, green as done below
            //TODO: Also this code definitely deserves to NOT be called every frame
            moveWithLevel[0].gameObject.GetComponent<PlayerControl>().roundsWon = whiteCatScore;
            moveWithLevel[1].gameObject.GetComponent<PlayerControl>().roundsWon = blueCatScore;
            moveWithLevel[2].gameObject.GetComponent<PlayerControl>().roundsWon = yellowCatScore;
            moveWithLevel[3].gameObject.GetComponent<PlayerControl>().roundsWon = greenCatScore;
        if(roundOver)
        {

            roundOverText.gameObject.SetActive(true);
            mainMenuButton.gameObject.SetActive(true);
            mainMenuButton.Select();
            if (level == 6)
            {
                // todo game over!!
            }
 
        }
        else
        {
            roundOverText.gameObject.SetActive(false);
            mainMenuButton.gameObject.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Escape) || InputManager.ActiveDevice.CommandWasPressed)
        {
            if (!roundOver)
            {
                if (isPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }

        }
        else if (CatIndex.controllersConnected > InputManager.Devices.Count)
        {
            Time.timeScale = 0;
            pause.SetActive(true);
        }
        else
        {
            if (!GameManager.isPaused && !GameManager.roundOver)
            {
                pause.SetActive(false);
                Time.timeScale = 1;
            }
        }


    }

    private float catLevelChange = 100.8f;
    private float camLevelChangeMin = 300;
    private float camLevelChangeMax = 600;
    public void levelOne()
    {
        upstairs.SetActive(false);
    }
    
    public void levelTwo()
    {
        float startAngle = UnityEngine.Random.Range(0, 360) * Mathf.Deg2Rad;
        float catAngle = 0;
        for (int i = 0; i < moveWithLevel.Capacity; i++)
        {
            if(moveWithLevel[i].gameObject.name.Equals("Crown"))
            {
                moveWithLevel[i].transform.position = new Vector3(
                    secondFloorSpawnCenter.x,
                    moveWithLevel[i].transform.position.y + catLevelChange,
                    secondFloorSpawnCenter.z
                    );
                Debug.Log("Crown moved\n");
            }
            else if(moveWithLevel[i].gameObject.tag.Equals("Player"))
            {
                Debug.Log("Cat moved\n");
                moveWithLevel[i].transform.position = new Vector3(
                    secondFloorSpawnCenter.x + startRadius * Mathf.Cos(startAngle + catAngle),
                    secondFloorSpawnCenter.y,
                    secondFloorSpawnCenter.z + startRadius * Mathf.Sin(startAngle + catAngle)
                );
                catAngle += Mathf.PI/2;
            }
            else
            {
                Vector3 pos = moveWithLevel[i].transform.position;
                pos.y = pos.y + catLevelChange;
                moveWithLevel[i].transform.position = pos;
            }
        }
        zhScript.minHeight = camLevelChangeMin;
        zhScript.maxHeight = camLevelChangeMax;
        puSpawnScript.powerUpYPosition += 100;
        upstairs.SetActive(true);
        dog.SetActive(true);
    }
    
    public static void EndRound(int catIdx)
    {
        roundOver = true;
        Time.timeScale = 0;
        //ROUND NUMBER IS INCREMENTED IN THE SCOREBOARD SCRIPT SO THAT IT IS 
        //NOT SHOWN WHEN GAME FREEZES AT CONCLUSION OF A ROUND

        /* 0 for white, 1 for blue, 2 for yellow, 3 for green */
        if (catIdx == 0)
        {
            whiteCatScore++;
            Debug.Log("white cat score " + whiteCatScore);
        }
        else if (catIdx == 1)
        {
            blueCatScore++;
            Debug.Log("blue cat score " + blueCatScore);
        }
        else if (catIdx == 2)
        {
            yellowCatScore++;
            Debug.Log("yellow cat score " + yellowCatScore);
        }
        else if(catIdx == 3)
        {
            greenCatScore++;
            Debug.Log("green cat score " + greenCatScore);
        }
        
        
    }

    public static void RestartGame()
    {
        Time.timeScale = 1;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        SceneManager.LoadScene(3);
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
        resumeButton.Select();
        Time.timeScale = 0f;
        GameManager.isPaused = true;
    }

}

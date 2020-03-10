using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scoreboard : MonoBehaviour
{

    public GameObject whiteCrown1;
    public GameObject whiteCrown2;
    public GameObject whiteWinCrown;

    public GameObject blueCrown1;
    public GameObject blueCrown2;
    public GameObject blueWinCrown;

    public GameObject yellowCrown1;
    public GameObject yellowCrown2;
    public GameObject yellowWinCrown;

    public GameObject greenCrown1;
    public GameObject greenCrown2;
    public GameObject greenWinCrown;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.roundNumber++;
        if (GameManager.whiteCatScore > 0)
        {
            whiteCrown1.gameObject.SetActive(true);
            if(GameManager.whiteCatScore == 2)
            {
                whiteCrown2.gameObject.SetActive(true);
                whiteWinCrown.gameObject.SetActive(true);
                GameManager.roundNumber = 1;
            }
        }

        if (GameManager.blueCatScore > 0)
        {
            blueCrown1.gameObject.SetActive(true);
            if (GameManager.blueCatScore == 2)
            {
                blueCrown2.gameObject.SetActive(true);
                blueWinCrown.gameObject.SetActive(true);
                GameManager.roundNumber = 1;
            }
        }

        if (GameManager.yellowCatScore > 0)
        {
            yellowCrown1.gameObject.SetActive(true);
            if (GameManager.yellowCatScore == 2)
            {
                yellowCrown2.gameObject.SetActive(true);
                yellowWinCrown.gameObject.SetActive(true);
                GameManager.roundNumber = 1;
            }
        }

        if (GameManager.greenCatScore > 0)
        {
            greenCrown1.gameObject.SetActive(true);
            if (GameManager.greenCatScore == 2)
            {
                greenCrown2.gameObject.SetActive(true);
                greenWinCrown.gameObject.SetActive(true);
                GameManager.roundNumber = 1;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextButton()
    {
        /*if(GameManager.gameOver)
        {
            SceneManager.SetActiveScene(GAMEOVER_SCENE_INDEX);
        } */

        SceneManager.LoadScene(2);
    }
}

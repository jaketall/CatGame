using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class AssignCats : MonoBehaviour
{
    private bool greenCat;
    private bool blueCat;
    private bool yellowCat;
    private bool whiteCat;

    InputDevice playerOne;
    InputDevice playerTwo;
    InputDevice playerThree;
    InputDevice playerFour;

    int readyCount;
    public bool playerOneReady;
    public bool playerTwoReady;
    public bool playerThreeReady;
    public bool playerFourReady;

    private bool playerOneAssigned;
    private bool playerTwoAssigned;
    private bool playerThreeAssigned;
    private bool playerFourAssigned;

    public GameObject greenCatText;
    public GameObject blueCatText;
    public GameObject yellowCatText;
    public GameObject whiteCatText;

    public GameObject greenCatConnected;
    public GameObject blueCatConnected;
    public GameObject yellowCatConnected;
    public GameObject whiteCatConnected;

    public static bool greenCatisReady;
    public static bool blueCatisReady;
    public static bool yellowCatisReady;
    public static bool whiteCatisReady;


    public GameObject A;
    public GameObject B;
    public GameObject Y;
    public GameObject X;

    public GameObject greenCatReady;
    public GameObject blueCatReady;
    public GameObject yellowCatReady;
    public GameObject whiteCatReady;

    private bool startGame;

    public Text startText;
    // Start is called before the first frame update
    void Start()
    {
        greenCatisReady = false;
        whiteCatisReady = false;
        blueCatisReady = false;
        yellowCatisReady = false;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        int numControllers = InputManager.Devices.Count;
        //Player one
        if(numControllers > 0)
        {
            CatIndex.controllersConnected = 1;
            playerOne = InputManager.Devices[0];
            if (!playerOneReady)
            {
                if (playerOne.Action1.WasPressed)
                {
                    if (!greenCat && !playerOneAssigned)
                    {
                        CatIndex.greenCatIndex = 0;
                        greenCat = true;
                        playerOneAssigned = true;
                    }

                }
                else if (playerOne.Action2.WasPressed)
                {
                    if (!blueCat && !playerOneAssigned)
                    {
                        CatIndex.blueCatIndex = 0;
                        blueCat = true;
                        playerOneAssigned = true;
                    }
                }
                else if (playerOne.Action4.WasPressed)
                {
                    if (!yellowCat && !playerOneAssigned)
                    {
                        CatIndex.yellowCatIndex = 0;
                        yellowCat = true;
                        playerOneAssigned = true;
                    }

                }
                else if (playerOne.Action3.WasPressed)
                {
                    if (!whiteCat && !playerOneAssigned)
                    {
                        CatIndex.whiteCatIndex = 0;
                        whiteCat = true;
                        playerOneAssigned = true;
                    }

                }
                else if (playerOne.LeftStickButton.WasPressed)
                {
                    if (yellowCat && CatIndex.yellowCatIndex == 0 && playerOneAssigned)
                    {
                        CatIndex.yellowCatIndex = -1;
                        yellowCat = false;
                        playerOneAssigned = false;
                    }
                    else if (blueCat && CatIndex.blueCatIndex == 0 && playerOneAssigned)
                    {
                        CatIndex.blueCatIndex = -1;
                        blueCat = false;
                        playerOneAssigned = false;
                    }
                    else if (greenCat && CatIndex.greenCatIndex == 0 && playerOneAssigned)
                    {
                        CatIndex.greenCatIndex = -1;
                        greenCat = false;
                        playerOneAssigned = false;
                    }
                    else if (whiteCat && CatIndex.whiteCatIndex == 0 && playerOneAssigned)
                    {
                        CatIndex.whiteCatIndex = -1;
                        whiteCat = false;
                        playerOneAssigned = false;
                    }
                }


                if (playerOne.CommandWasPressed && playerOneAssigned)
                {
                    playerOneReady = true;
                    readyUp(0);
                    readyCount++;
                }
            }
        }


        //Player two
        if(numControllers > 1)
        {
            CatIndex.controllersConnected = 2;
            playerTwo = InputManager.Devices[1];
            if (!playerTwoReady)
            {
                if (playerTwo.Action1.WasPressed)
                {
                    if (!greenCat && !playerTwoAssigned)
                    {
                        CatIndex.greenCatIndex = 1;
                        greenCat = true;
                        playerTwoAssigned = true;
                    }

                }
                else if (playerTwo.Action2.WasPressed)
                {
                    if (!blueCat && !playerTwoAssigned)
                    {
                        CatIndex.blueCatIndex = 1;
                        blueCat = true;
                        playerTwoAssigned = true;
                    }
                }
                else if (playerTwo.Action4.WasPressed)
                {
                    if (!yellowCat && !playerTwoAssigned)
                    {
                        CatIndex.yellowCatIndex = 1;
                        yellowCat = true;
                        playerTwoAssigned = true;
                    }
                }
                else if (playerTwo.Action3.WasPressed)
                {
                    if (!whiteCat && !playerTwoAssigned)
                    {
                        CatIndex.whiteCatIndex = 1;
                        whiteCat = true;
                        playerTwoAssigned = true;
                    }
                }
                else if (playerTwo.LeftStickButton.WasPressed)
                {
                    if (greenCat && CatIndex.greenCatIndex == 1 && playerTwoAssigned)
                    {
                        CatIndex.greenCatIndex = -1;
                        greenCat = false;
                        playerTwoAssigned = false;
                    }
                    else if (blueCat && CatIndex.blueCatIndex == 1 && playerTwoAssigned)
                    {
                        CatIndex.blueCatIndex = -1;
                        blueCat = false;
                        playerTwoAssigned = false;
                    }
                    else if (yellowCat && CatIndex.yellowCatIndex == 1 && playerTwoAssigned)
                    {
                        CatIndex.yellowCatIndex = -1;
                        yellowCat = false;
                        playerTwoAssigned = false;
                    }
                    else if (whiteCat && CatIndex.whiteCatIndex == 1 && playerTwoAssigned)
                    {
                        CatIndex.whiteCatIndex = -1;
                        whiteCat = false;
                        playerTwoAssigned = false;
                    }
                }


                if (playerTwo.CommandWasPressed && playerTwoAssigned)
                {
                    playerTwoReady = true;
                    readyUp(1);
                    readyCount++;
                }
            }
        }

        if(numControllers > 2)
        {
            CatIndex.controllersConnected = 3;
            playerThree = InputManager.Devices[2];
            //Player 3
            if (!playerThreeReady)
            {
                if (playerThree.Action1.WasPressed)
                {
                    if (!greenCat && !playerThreeAssigned)
                    {
                        CatIndex.greenCatIndex = 2;
                        greenCat = true;
                        playerThreeAssigned = true;
                    }

                }
                else if (playerThree.Action2.WasPressed)
                {
                    if (!blueCat && !playerThreeAssigned)
                    {
                        CatIndex.blueCatIndex = 2;
                        blueCat = true;
                        playerThreeAssigned = true;
                    }
                }
                else if (playerThree.Action4.WasPressed)
                {
                    if (!yellowCat && !playerThreeAssigned)
                    {
                        CatIndex.yellowCatIndex = 2;
                        yellowCat = true;
                        playerThreeAssigned = true;
                    }

                }
                else if (playerThree.Action3.WasPressed)
                {
                    if (!whiteCat && !playerThreeAssigned)
                    {
                        CatIndex.whiteCatIndex = 2;
                        whiteCat = true;
                        playerThreeAssigned = true;
                    }

                }
                else if (playerThree.LeftStickButton.WasPressed)
                {
                    if (greenCat && CatIndex.greenCatIndex == 2 && playerThreeAssigned)
                    {
                        CatIndex.greenCatIndex = -1;
                        greenCat = false;
                        playerThreeAssigned = false;
                    }
                    else if (blueCat && CatIndex.blueCatIndex == 2 && playerThreeAssigned)
                    {
                        CatIndex.blueCatIndex = -1;
                        blueCat = false;
                        playerThreeAssigned = false;
                    }
                    else if (yellowCat && CatIndex.yellowCatIndex == 2 && playerThreeAssigned)
                    {
                        CatIndex.yellowCatIndex = -1;
                        yellowCat = false;
                        playerThreeAssigned = false;
                    }
                    else if (whiteCat && CatIndex.whiteCatIndex == 2 && playerThreeAssigned)
                    {
                        CatIndex.whiteCatIndex = -1;
                        whiteCat = false;
                        playerThreeAssigned = false;
                    }
                }


                if (playerThree.CommandWasPressed && playerThreeAssigned)
                {
                    playerThreeReady = true;
                    readyUp(2);
                    readyCount++;
                }
            }
        }

        if(numControllers > 3)
        {
            CatIndex.controllersConnected = 4;
            //Player 4
            playerFour = InputManager.Devices[3];
            if (!playerFourReady)
            {
                if (playerFour.Action1.WasPressed)
                {
                    if (!greenCat && !playerFourAssigned)
                    {
                        CatIndex.greenCatIndex = 3;
                        greenCat = true;
                        playerFourAssigned = true;
                    }

                }
                else if (playerFour.Action2.WasPressed)
                {
                    if (!blueCat && !playerFourAssigned)
                    {
                        CatIndex.blueCatIndex = 3;
                        blueCat = true;
                        playerFourAssigned = true;
                    }
                }
                else if (playerFour.Action4.WasPressed)
                {
                    if (!yellowCat && !playerFourAssigned)
                    {
                        CatIndex.yellowCatIndex = 3;
                        yellowCat = true;
                        playerFourAssigned = true;
                    }
                }
                else if (playerFour.Action3.WasPressed)
                {
                    if (!whiteCat && !playerFourAssigned)
                    {
                        CatIndex.whiteCatIndex = 3;
                        whiteCat = true;
                        playerFourAssigned = true;
                    }

                }
                else if (playerFour.LeftStickButton.WasPressed)
                {
                    if (greenCat && CatIndex.greenCatIndex == 3 && playerFourAssigned)
                    {
                        CatIndex.greenCatIndex = -1;
                        greenCat = false;
                        playerFourAssigned = false;
                    }
                    else if (blueCat && CatIndex.blueCatIndex == 3 && playerFourAssigned)
                    {
                        CatIndex.blueCatIndex = -1;
                        blueCat = false;
                        playerFourAssigned = false;
                    }
                    else if (yellowCat && CatIndex.yellowCatIndex == 3 && playerFourAssigned)
                    {
                        CatIndex.yellowCatIndex = -1;
                        yellowCat = false;
                        playerFourAssigned = false;
                    }
                    else if (whiteCat && CatIndex.whiteCatIndex == 3 && playerFourAssigned)
                    {
                        CatIndex.whiteCatIndex = -1;
                        whiteCat = false;
                        playerFourAssigned = false;
                    }
                }

                if (playerFour.CommandWasPressed && playerFourAssigned)
                {
                    playerFourReady = true;
                    readyUp(3);
                    readyCount++;
                }
            }
        }
        if (greenCat)
        {
            greenCatText.SetActive(false);
            A.SetActive(false);
            if(!greenCatisReady)
                greenCatConnected.SetActive(true);
        }
        else
        {
            greenCatText.SetActive(true);
            A.SetActive(true);
            greenCatConnected.SetActive(false);
        }

        if (whiteCat)
        {
            whiteCatText.SetActive(false);
            X.SetActive(false);
            if(!whiteCatisReady)
                whiteCatConnected.SetActive(true);
        }
        else
        {
            whiteCatText.SetActive(true);
            X.SetActive(true);
            whiteCatConnected.SetActive(false);
        }

        if (blueCat)
        {
            blueCatText.SetActive(false);
            B.SetActive(false);
            if(!blueCatisReady)
                blueCatConnected.SetActive(true);
        }
        else
        {
            blueCatText.SetActive(true);
            B.SetActive(true);
            blueCatConnected.SetActive(false);
        }

        if (yellowCat)
        {
            yellowCatText.SetActive(false);
            Y.SetActive(false);
            if(!yellowCatisReady)
                yellowCatConnected.SetActive(true);
        }
        else
        {
            yellowCatText.SetActive(true);
            Y.SetActive(true);
            yellowCatConnected.SetActive(false);
        }

        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if(readyCount > 0 && readyCount == numControllers)
        {
            
            startText.text = "Press Start to Play";
            startText.color = new Color(0, 0, 255);
            if (InputManager.ActiveDevice.CommandWasPressed && startGame)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            startGame = true;
        }


    }

    void readyUp(int player)
    {
        if(greenCat && CatIndex.greenCatIndex == player)
        {
            greenCatReady.SetActive(true);
            greenCatConnected.SetActive(false);
            greenCatisReady = true;
        }
        else if (blueCat && CatIndex.blueCatIndex == player)
        {
            blueCatReady.SetActive(true);
            blueCatConnected.SetActive(false);
            blueCatisReady = true;
        }
        else if (yellowCat && CatIndex.yellowCatIndex == player)
        {
            yellowCatReady.SetActive(true);
            yellowCatConnected.SetActive(false);
            yellowCatisReady = true;
        }
        else if (whiteCat && CatIndex.whiteCatIndex == player)
        {
            whiteCatReady.SetActive(true);
            whiteCatConnected.SetActive(false);
            whiteCatisReady = true;
        }
    }
}

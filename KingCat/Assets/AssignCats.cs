﻿using System.Collections;
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

    public bool playerOneAssigned;
    private bool playerTwoAssigned;
    private bool playerThreeAssigned;
    private bool playerFourAssigned;

    public TextMeshProUGUI greenCatText;
    public TextMeshProUGUI blueCatText;
    public TextMeshProUGUI yellowCatText;
    public TextMeshProUGUI whiteCatText;

    public GameObject A;
    public GameObject B;
    public GameObject Y;
    public GameObject X;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int numControllers = InputManager.Devices.Count;
        //Player one
        if(numControllers > 0)
        {
            CatIndex.controllersConnected = 1;
            playerOne = InputManager.Devices[0];
            if (playerOne.Action1.WasPressed)
            {
                if (!greenCat && !playerOneAssigned)
                {
                    CatIndex.greenCatIndex = 0;
                    greenCat = true;
                    playerOneAssigned = true;
                }
                else if(greenCat && CatIndex.greenCatIndex == 0 && playerOneAssigned)
                {
                    CatIndex.greenCatIndex = -1;
                    greenCat = false;
                    playerOneAssigned = false;
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
                else if (blueCat && CatIndex.blueCatIndex == 0 && playerOneAssigned)
                {
                    CatIndex.blueCatIndex = -1;
                    blueCat = false;
                    playerOneAssigned = false;
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
                else if (yellowCat && CatIndex.yellowCatIndex == 0 && playerOneAssigned)
                {
                    CatIndex.yellowCatIndex = -1;
                    yellowCat = false;
                    playerOneAssigned = false;
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
                else if (whiteCat && CatIndex.whiteCatIndex == 0 && playerOneAssigned)
                {
                    CatIndex.whiteCatIndex = -1;
                    whiteCat = false;
                    playerOneAssigned = false;
                }
            }

            if (playerOne.CommandWasPressed)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }


        //Player two
        if(numControllers > 1)
        {
            CatIndex.controllersConnected = 2;
            playerTwo = InputManager.Devices[1];
            if (playerTwo.Action1.WasPressed)
            {
                if (!greenCat && !playerTwoAssigned)
                {
                    CatIndex.greenCatIndex = 1;
                    greenCat = true;
                    playerTwoAssigned = true;
                }
                else if (greenCat && CatIndex.greenCatIndex == 1 && playerTwoAssigned)
                {
                    CatIndex.greenCatIndex = -1;
                    greenCat = false;
                    playerTwoAssigned = false;
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
                else if (blueCat && CatIndex.blueCatIndex == 1 && playerTwoAssigned)
                {
                    CatIndex.blueCatIndex = -1;
                    blueCat = false;
                    playerTwoAssigned = false;
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
                else if (yellowCat && CatIndex.yellowCatIndex == 1 && playerTwoAssigned)
                {
                    CatIndex.yellowCatIndex = -1;
                    yellowCat = false;
                    playerTwoAssigned = false;
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
                else if (whiteCat && CatIndex.whiteCatIndex == 1 && playerTwoAssigned)
                {
                    CatIndex.whiteCatIndex = -1;
                    whiteCat = false;
                    playerTwoAssigned = false;
                }
            }

            if (playerTwo.CommandWasPressed)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }

        if(numControllers > 2)
        {
            CatIndex.controllersConnected = 3;
            playerThree = InputManager.Devices[2];
            //Player 3
            if (playerThree.Action1.WasPressed)
            {
                if (!greenCat && !playerThreeAssigned)
                {
                    CatIndex.greenCatIndex = 2;
                    greenCat = true;
                    playerThreeAssigned = true;
                }
                else if (greenCat && CatIndex.greenCatIndex == 2 && playerThreeAssigned)
                {
                    CatIndex.greenCatIndex = -1;
                    greenCat = false;
                    playerThreeAssigned = false;
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
                else if (blueCat && CatIndex.blueCatIndex == 2 && playerThreeAssigned)
                {
                    CatIndex.blueCatIndex = -1;
                    blueCat = false;
                    playerThreeAssigned = false;
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
                else if (yellowCat && CatIndex.yellowCatIndex == 2 && playerThreeAssigned)
                {
                    CatIndex.yellowCatIndex = -1;
                    yellowCat = false;
                    playerThreeAssigned = false;
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
                else if (whiteCat && CatIndex.whiteCatIndex == 2 && playerThreeAssigned)
                {
                    CatIndex.whiteCatIndex = -1;
                    whiteCat = false;
                    playerThreeAssigned = false;
                }
            }

            if (playerThree.CommandWasPressed)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }

        if(numControllers > 3)
        {
            CatIndex.controllersConnected = 4;
            //Player 4
            playerFour = InputManager.Devices[3];
            if (playerFour.Action1.WasPressed)
            {
                if (!greenCat && !playerFourAssigned)
                {
                    CatIndex.greenCatIndex = 3;
                    greenCat = true;
                    playerFourAssigned = true;
                }
                else if (greenCat && CatIndex.greenCatIndex == 3 && playerFourAssigned)
                {
                    CatIndex.greenCatIndex = -1;
                    greenCat = false;
                    playerFourAssigned = false;
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
                else if (blueCat && CatIndex.blueCatIndex == 3 && playerFourAssigned)
                {
                    CatIndex.blueCatIndex = -1;
                    blueCat = false;
                    playerFourAssigned = false;
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
                else if (yellowCat && CatIndex.yellowCatIndex == 3 && playerFourAssigned)
                {
                    CatIndex.yellowCatIndex = -1;
                    yellowCat = false;
                    playerFourAssigned = false;
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
                else if (whiteCat && CatIndex.whiteCatIndex == 3 && playerFourAssigned)
                {
                    CatIndex.whiteCatIndex = -1;
                    whiteCat = false;
                    playerFourAssigned = false;
                }
            }
            if (playerFour.CommandWasPressed)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }


        if (greenCat)
        {
            greenCatText.text = "Connected";
            A.SetActive(false);
        }
        else
        {
            greenCatText.text = "Press";
            A.SetActive(true);
        }

        if (whiteCat)
        {
            whiteCatText.text = "Connected";
            X.SetActive(false);
        }
        else
        {
            whiteCatText.text = "Press";
            X.SetActive(true);
        }

        if (blueCat)
        {
            blueCatText.text = "Connected";
            B.SetActive(false);
        }
        else
        {
            blueCatText.text = "Press";
            B.SetActive(true);
        }

        if (yellowCat)
        {
            yellowCatText.text = "Connected";
            Y.SetActive(false);
        }
        else
        {
            yellowCatText.text = "Press";
            Y.SetActive(true);
        }

        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }


    }
}

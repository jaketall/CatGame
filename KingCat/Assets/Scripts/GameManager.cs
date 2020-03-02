using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static bool gameOver;
    public static int roundNumber;
    public static bool roundOver;

    public static int blueCatScore;
    public static int yellowCatScore;
    public static int greenCatScore;
    public static int redCatScore;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void EndRound()
    {
        roundOver = true;
    }
}

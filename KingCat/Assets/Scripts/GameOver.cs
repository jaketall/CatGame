using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    public GameObject white;
    public GameObject blue;
    public GameObject yellow;
    public GameObject green;
    
    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.whiteCatScore == 2)
        {
            white.gameObject.SetActive(true);
        }
        else if(GameManager.blueCatScore == 2)
        {
            blue.gameObject.SetActive(true);
        }
        else if(GameManager.yellowCatScore == 2)
        {
            yellow.gameObject.SetActive(true);
        }
        else if(GameManager.greenCatScore == 2)
        {
            green.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}

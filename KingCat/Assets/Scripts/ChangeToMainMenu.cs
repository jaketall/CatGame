using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeToMainMenu : MonoBehaviour
{
    private GameObject catidx;
    private GameObject incontrol;
    public void ToMainMenu()
    {
        catidx = GameObject.Find("CatIndex");
        if (catidx)
        {
            Debug.Log("destroying catidx");
            Destroy(catidx);
        }
        SceneManager.LoadScene(0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeToMainMenu : MonoBehaviour
{
    private GameObject catidx;
    private GameObject music;
    private GameObject incontrol;
    public void ToMainMenu()
    {
        catidx = GameObject.Find("CatIndex");
        if (catidx)
        {
            Debug.Log("destroying catidx");
            Destroy(catidx);
        }
        music = GameObject.Find("Game Music");
        if (music)
        {
            Destroy(music);
        }
        SceneManager.LoadScene(0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    public Text t;
    public static bool gameStarted;
    private bool countDownFinish;
    // Start is called before the first frame update
    void Start()
    {
        countDownFinish = false;
        t.enabled = true;
        gameStarted = false;
        t.text = "3";

    }

    // Update is called once per frame
    void Update()
    {
        if (!countDownFinish)
        {
            StartCoroutine(countDown());
            countDownFinish = true;
        }


    }
    IEnumerator countDown()
    {
        yield return new WaitForSeconds(1f);
        t.text = "2";
        yield return new WaitForSeconds(1f);
        t.text = "1";
        yield return new WaitForSeconds(1f);
        t.text = "Go!";
        yield return new WaitForSeconds(0.3f);
        t.enabled = false;
        gameStarted = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBarUI : MonoBehaviour
{

    public GameObject cat;
    public Image scoreBar;
    public Text scoreText;
    private Transform crownIcon;

    private void Start()
    {
        crownIcon = transform.Find("Crown Icon");
        UpdateScoreBar();
    }

    private void UpdateScoreBar()
    {
        
        float ratio = cat.GetComponent<PlayerControl>().currentScore / GameManager.maxScore;
        if(ratio >= 1.0f)
        {
            ratio = 1.0f;
            //cat.GetComponent<PlayerControl>().roundsWon++;
            //end round
            cat.GetComponent<PlayerControl>().currentScore = GameManager.maxScore;
        }
        scoreBar.rectTransform.localScale = new Vector3(ratio, 1, 1);
        if(cat.GetComponent<PlayerControl>().hasCrown)
        {
            crownIcon.gameObject.SetActive(true);
        }
        else
        {
            crownIcon.gameObject.SetActive(false);
        }
        scoreText.text = ((int)cat.GetComponent<PlayerControl>().currentScore).ToString();
    }

    private void Update()
    {
        UpdateScoreBar();
    }

}

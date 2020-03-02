using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBarUI : MonoBehaviour
{

    public GameObject cat;
    public Image scoreBar;
    public Text scoreText;
    public GameManager gameManager;

    public float maxScore = 60;

    private void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        UpdateScoreBar();
    }

    private void UpdateScoreBar()
    {
        
        float ratio = cat.GetComponent<PlayerControl>().currentScore / maxScore;
        if(ratio >= 1.0f)
        {
            ratio = 1.0f;
            cat.GetComponent<PlayerControl>().roundsWon++;
            //end round
            cat.GetComponent<PlayerControl>().currentScore = 60;
        }
        scoreBar.rectTransform.localScale = new Vector3(ratio, 1, 1);
        scoreText.text = ((int)cat.GetComponent<PlayerControl>().currentScore).ToString();
    }

    private void Update()
    {
        UpdateScoreBar();
    }



    /*public GameObject Cat;
    public int maxScore;
    public int currentScore;
    public Slider healthFill;
    public float healthBarYOffset = 5;
    public Image hBcolor;
    public float alpha = 0.6f;
    private Color green_;
    private Color yellow_;
    private Color red_;
    // Start is called before the first frame update
    void Start()
    {
        //dead = false;
        green_ = new Color(0, 1, 0, alpha);
        yellow_ = new Color(1, 0.92f, 0.016f, alpha);
        red_ = new Color(1, 0, 0, alpha);
        hBcolor.color = green_; 


    }

    // Update is called once per frame
    void Update()
    {
        if (!gameObject.CompareTag("Player"))
            PositionHealthBar();

        if (Cat.GetComponent<PlayerControl>().hasCrown)
        {
            currentScore += (int)Time.deltaTime;
        }
        healthFill.value = currentScore;
    }
    */

    /*public void ModifyHealth(int amount)
    {
        if (currentHealth + amount >= maxScore)
        {
            currentHealth = maxScore;
        }
        else if (currentHealth + amount <= 0)
        {
            currentHealth = 0;
        }
        else
        {
            currentHealth += amount;
        }

        if (currentHealth >= 70)
        {
            hBcolor.color = green_;
        }
        else if (currentHealth >= 35)
        {
            hBcolor.color = yellow_;
        }
        else
        {
            hBcolor.color = red_;
        }

        healthFill.value = (float)currentHealth / (float)maxScore;
        if (currentHealth <= 0)
        {
            dead = true;
        }

    } */

    /*private void PositionHealthBar()
    {
        Vector3 currentPos = transform.position;
        healthBar.position = new Vector3(currentPos.x, currentPos.y +
            healthBarYOffset, currentPos.z);
        healthBar.LookAt(Camera.main.transform);
    }*/







    /*public float barDisplay;
    private Vector2 pos = new Vector2(Screen.width - 255, Screen.height - 90); //Screen.width - (Screen.width/8), Screen.height - (Screen.height/14) some type of ratio
    public Vector2 size = new Vector2(200, 30);
    public Texture2D emptyTex;
    public Texture2D fullTex;
    public GameObject Cat;
    public float fillSpeed = 0.03f;
    //public Component catImage;

    
    // Start is called before the first frame update
    void Start()
    {
        barDisplay = 0;
        //catImage = gameObject.GetComponent("Image");
        if(gameObject.CompareTag("Player2Bar"))
        {
            pos = new Vector2(Screen.width - 255, Screen.height - 45);
        }
        Debug.Log("Changed cat images' positions.");
        transform.position = new Vector2(pos.x - 25, Screen.height - pos.y - 20);
    }

    // Update is called once per frame

    void OnGUI()
    {
        GUI.BeginGroup(new Rect(pos.x, pos.y, size.x, size.y));

        GUI.Box(new Rect(0, 0, size.x, size.y), emptyTex);

        GUI.BeginGroup(new Rect(0, 0, size.x * barDisplay, size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), fullTex);
        GUI.EndGroup();
        GUI.EndGroup();
    }


    void Update()
    {
        if (Cat.GetComponent<PlayerControl>().hasCrown)
        {
            barDisplay += Time.deltaTime * fillSpeed;
        }
    } */



}

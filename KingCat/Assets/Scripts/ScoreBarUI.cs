using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBarUI : MonoBehaviour
{

    public float barDisplay;
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
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatIndex : MonoBehaviour
{
    public static int blueCatIndex = -1;
    public static int whiteCatIndex = -1;
    public static int yellowCatIndex = -1;
    public static int greenCatIndex = -1;
    public static int controllersConnected = 0;
	// Start is called before the first frame update
	void Start()
    {
        blueCatIndex = -1;
        whiteCatIndex = -1;
        yellowCatIndex = -1;
        greenCatIndex = -1;
        controllersConnected = 0;
        DontDestroyOnLoad(gameObject);
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}

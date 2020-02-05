using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float xClamp;
    public float yClamp;
    public float speed; 
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal_input = Input.GetAxis("Horizontal");
        float vertical_input = Input.GetAxis("Vertical");
        
        transform.Translate(Time.deltaTime * horizontal_input * Vector3.right * speed);
        transform.Translate(Time.deltaTime * vertical_input * Vector3.forward * speed);
        
    }
}

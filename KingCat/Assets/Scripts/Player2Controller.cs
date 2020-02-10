using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Controller : MonoBehaviour
{
    public float xClamp;
    public float yClamp;
    public float thrust; 
    float horizontal_input = 0;
    float vertical_input = 0;
    private Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    { 
        rb = GetComponent<Rigidbody>();   
    }

    // Update is called once per frame
    void Update()
    {
  
        if (Input.GetKey(KeyCode.J))
        {
            horizontal_input = -1;
        }
        else if (Input.GetKey(KeyCode.L))
        {
            horizontal_input = 1;
        }
        else
        {
            horizontal_input = 0;
        }

        if (Input.GetKey(KeyCode.I))
        {
            vertical_input = 1;
        }
      
        else if (Input.GetKey(KeyCode.K))
        {
            vertical_input = -1;
        }
        
        else
        {
            vertical_input = 0;
        }

       
        
        rb.AddForce(thrust * horizontal_input * Vector3.right);
        rb.AddForce(thrust * vertical_input * Vector3.forward);
        // transform.Translate(Time.deltaTime * horizontal_input * Vector3.right * speed);
        // transform.Translate(Time.deltaTime * vertical_input * Vector3.forward * speed);
        
    }
}
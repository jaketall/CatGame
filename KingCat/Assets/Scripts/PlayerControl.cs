﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float xClamp;
    public float yClamp;
    public float thrust; 
    private Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    { 
        rb = GetComponent<Rigidbody>();   
    }

    // Update is called once per frame
    void Update()
    {

        //rb.AddForce(thrust * horizontal_input * Vector3.right);
        //rb.AddForce(thrust * vertical_input * Vector3.forward);
        // transform.Translate(Time.deltaTime * horizontal_input * Vector3.right * speed);
        // transform.Translate(Time.deltaTime * vertical_input * Vector3.forward * speed);


    }
    private void FixedUpdate()
    {
        float horizontal_input = Input.GetAxis("Horizontal");
        float vertical_input = Input.GetAxis("Vertical");
        Vector3 newPos = new Vector3(horizontal_input, 0, vertical_input) * thrust;
        rb.MovePosition(transform.position + newPos);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("powerup"))
        {
            Destroy(other.gameObject);          
        }
    }
}

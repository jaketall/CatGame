﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectMe : MonoBehaviour
{
    public GameObject[] targets;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] nearbyObjects = Physics.OverlapSphere(transform.position, transform.localScale.x); 
        GameObject closestPlayer = null; //used for resolving frame-perfect ties.
        for(int j=0; j<nearbyObjects.Length;j++)
        {
            for(int i=0;i<targets.Length;i++)
            {
                if(nearbyObjects[j].gameObject.name == targets[i].name)
                {
                    if(closestPlayer != null)
                    {
                        if(Vector3.Distance(targets[i].transform.position,transform.position) 
                            < Vector3.Distance(closestPlayer.transform.position, transform.position))
                        {
                            closestPlayer = targets[i];
                        }
                    }
                    else
                    {
                        closestPlayer = targets[i];
                    }
                }
            }
        }
        if(closestPlayer != null)
            Destroy(this.gameObject);
    }
}

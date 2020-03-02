using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunController : MonoBehaviour
{
    public float radius;
    public float numSecondsPerDay;
    public GameObject center;

    private float angle;

    // Start is called before the first frame update
    void Start()
    {
        angle=0;
    }

    // Update is called once per frame
    void Update()
    {
        //angle needs to go from 0 -> 3.14
        //at a rate of numSecondsPerDay
        angle +=  Mathf.PI * Time.deltaTime / numSecondsPerDay;
        if (angle > Mathf.PI)
            angle = 0;
        transform.position = new Vector3(
        Mathf.Cos(angle)*radius + center.transform.position.x,
        Mathf.Sin(angle)*radius + center.transform.position.y,
        0 + center.transform.position.z );
    
        transform.LookAt(center.transform);

    }
}

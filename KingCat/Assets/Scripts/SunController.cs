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
        angle=Mathf.PI/4;
    }

    // Update is called once per frame
    void Update()
    {
        //angle needs to go from 0 -> 3.14
        //at a rate of numSecondsPerDay
        angle +=  Mathf.PI * Time.deltaTime / numSecondsPerDay;
        if (angle > 3*Mathf.PI/4)
            angle = Mathf.PI/4;
        transform.position = new Vector3(
        Mathf.Cos(angle)*radius + center.transform.position.x,
        Mathf.Sin(angle)*radius + center.transform.position.y,
        0 + center.transform.position.z );
    
        transform.LookAt(center.transform);

    }
}

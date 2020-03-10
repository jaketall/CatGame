using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairBallSelfDestruct : MonoBehaviour
{
    public float waitTime = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(destroyHairBall());
    }

    IEnumerator destroyHairBall()
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoccerBallScript : MonoBehaviour
{
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(GetComponent<Rigidbody>().velocity.magnitude);
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject player = collision.gameObject;
        if (player.CompareTag("Player"))
        {
            if(rb.velocity.magnitude > 50 && !player.GetComponent<PlayerControl>().isDashing)
            {
                player.GetComponent<Animator>().SetBool("Was_Hit", true);
                player.GetComponent<PlayerControl>().dropCrown(player);
            }
                

        }
    }
}

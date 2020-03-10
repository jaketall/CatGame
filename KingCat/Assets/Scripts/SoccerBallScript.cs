using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoccerBallScript : MonoBehaviour
{
    private Rigidbody rb;
    public ParticleSystem fire;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        if(rb.velocity.magnitude > 50)
        {
            if (!fire.isPlaying)
            {
                fire.Play();
            }
        }
        else
        {
            if (fire.isPlaying)
            {
                fire.Stop();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject player = collision.gameObject;
        if (player.CompareTag("Player"))
        {
            if(rb.velocity.magnitude > 50 && !player.GetComponent<PlayerControl>().isDashing)
            {
                player.GetComponent<PlayerControl>().setStun(false, 0);
                player.GetComponent<PlayerControl>().dropCrown(player);
            }
                

        }
    }
}

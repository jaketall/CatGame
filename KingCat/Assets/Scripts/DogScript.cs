using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class DogScript : MonoBehaviour
{
    private NavMeshAgent dog;
    public GameObject crown;
    private Vector3 dogHousePosition;
    public static bool goGetEmBoy;
    // Start is called before the first frame update
    void Start()
    {
        dog = GetComponent<NavMeshAgent>();
        dogHousePosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            goGetEmBoy = true;
        }
        if (goGetEmBoy)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject player in players)
            {
                if (player.GetComponent<PlayerControl>().hasCrown)
                { 
                    dog.SetDestination(player.transform.position);
                    return;
                }   
            }
        }
        else
        {
            dog.SetDestination(dogHousePosition);
        }


    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") &&
            collision.gameObject.GetComponent<PlayerControl>().hasCrown
            && goGetEmBoy)
        {
            collision.gameObject.GetComponent<PlayerControl>().dropCrown(collision.gameObject);
            goGetEmBoy = false;
        }
    }
}

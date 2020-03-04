using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class DogScript : MonoBehaviour
{
    private NavMeshAgent dog;
    public GameObject crown;
    private Vector3 dogHousePosition;
    // Start is called before the first frame update
    void Start()
    {
        dog = GetComponent<NavMeshAgent>();
        dogHousePosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject player in players){
            if (player.GetComponent<PlayerControl>().hasCrown)
            {
                dog.SetDestination(player.transform.position);
                return;
            }
        }
        dog.SetDestination(dogHousePosition);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerControl>().dropCrown(collision.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class DogScript : MonoBehaviour
{
    private NavMeshAgent dog;
    public GameObject crown;
    public Vector3 dogHousePosition;
    public static bool goGetEmBoy;
    private Animator dogAnim;
    private int runHash = Animator.StringToHash("Run");
    // Start is called before the first frame update
    void Start()
    {
        dog = GetComponent<NavMeshAgent>();
        dogHousePosition = transform.position;
        goGetEmBoy = false;
        dogAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            goGetEmBoy = true;
        }
        for (int i = 0; i < dog.path.corners.Length - 1; i++)
        {
            Debug.DrawLine(dog.path.corners[i], dog.path.corners[i + 1], Color.red);
        }
        if (goGetEmBoy)
        {
            dog.isStopped = false;
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject player in players)
            {
                if (player.GetComponent<PlayerControl>().hasCrown)
                {
                    dogAnim.SetBool(runHash, true);
                    dog.SetDestination(player.transform.position);
                    return;
                }
                dog.SetDestination(dogHousePosition);
                if(dog.remainingDistance < 1)
                {
                    dogAnim.SetBool(runHash, false);
                    dog.isStopped = true;
                }
                else
                {
                    dogAnim.SetBool(runHash, true);
                }
            }
        }
        else
        {
            dog.SetDestination(dogHousePosition);
            if (dog.remainingDistance < 2)
            {
                dogAnim.SetBool(runHash, false);
                dog.isStopped = true;
            }
            else
            {
                dogAnim.SetBool(runHash, true);
            }
        } 


    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") &&
            collision.gameObject.GetComponent<PlayerControl>().hasCrown
            && goGetEmBoy)
        {
            collision.gameObject.GetComponent<PlayerControl>().dropCrown(collision.gameObject);
            collision.gameObject.GetComponent<PlayerControl>().setStun(false, 0);
            collision.gameObject.GetComponent<PlayerControl>().catAudio.PlayOneShot(
                    collision.gameObject.GetComponent<PlayerControl>().stunSound);
            goGetEmBoy = false;
        }
    }
}

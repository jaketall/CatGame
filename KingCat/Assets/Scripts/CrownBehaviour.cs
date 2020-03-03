using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrownBehaviour : MonoBehaviour
{
    public static bool pickedUp;
    // Start is called before the first frame update
    void Start()
    {
        pickedUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!other.gameObject.GetComponent<PlayerControl>().hasCrown &&
                !pickedUp && other.gameObject.GetComponent<PlayerControl>().canPickupCrown)
            {
                /* if the player does not have the crown, the crown is not
                 * already picked up, and the player can pickup the crown */
                GetComponent<Rigidbody>().isKinematic = true;
                GetComponent<Rigidbody>().detectCollisions = false;
                /* basically inactivate rigidbody */
                transform.SetParent(other.gameObject.transform);
                transform.localPosition = new Vector3(0, 0.5f, 0.2f);
                /* move the crown over the cat's head */ 
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                // if the crown is moving, stop it
                other.gameObject.GetComponent<PlayerControl>().hasCrown = true;
                pickedUp = true;
                other.gameObject.GetComponent<PlayerControl>().canPickupCrown = false;
            }
        }
    }
}

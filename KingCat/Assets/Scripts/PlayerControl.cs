using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float xClamp;
    public float yClamp;
    public float thrust;

    private Rigidbody rb;

    public bool hasCrown;
    public GameObject crown;
    public float crownDropForce = 100; //how far the crown goes when dropped
    public float crownPickupDelay = 0.3f;
    public bool canPickupCrown; //used in the "stun" function


    private Animator catAnim;
    private int runHash = Animator.StringToHash("Run");
    private int dashHash = Animator.StringToHash("Dash");
    private int wasHitHash = Animator.StringToHash("Was_Hit");

    private bool isDashing;
    public float dashForce;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        catAnim = GetComponent<Animator>();
        canPickupCrown = true;
    }

    private void FixedUpdate()
    {
        catAnim.SetBool(dashHash, false);
        float horizontal_input = Input.GetAxis("Horizontal");
        float vertical_input = Input.GetAxis("Vertical");
        Vector3 newPos = new Vector3(horizontal_input, 0, vertical_input) * thrust;
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //used for testing, can remove when inputs are working
            dropCrown(gameObject);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isDashing)
            {
                //dash
                //later we can set a delay for dashing
                catAnim.SetBool(dashHash, true);
                isDashing = true;
                StartCoroutine(Dash());
            }
        }
        else if (!newPos.Equals(Vector3.zero))
        {
            // run
            if (!isDashing)
            {
                catAnim.SetBool(runHash, true);
                rb.MovePosition(transform.position + newPos);
                rb.MoveRotation(Quaternion.LookRotation(newPos));
            }
        }
        else
        {
            // not moving -> back to idle
            catAnim.SetBool(runHash, false);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("powerup"))
        {
            Destroy(other.gameObject);          
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (isDashing)
            {
                collision.gameObject.GetComponent<Animator>().SetTrigger(
                    wasHitHash);
                dropCrown(collision.gameObject);
            }
        }
    }

    IEnumerator Dash()
    {
        yield return new WaitForSeconds(0.3f);
        rb.AddRelativeForce(Vector3.forward * dashForce, ForceMode.Impulse);
        yield return new WaitForSeconds(0.3f);
        rb.velocity = Vector3.zero;
        isDashing = false;

    }
    IEnumerator Stun()
    {
        //delay pickup, so you can't pick it right back up after being stunned
        canPickupCrown = false;
        yield return new WaitForSeconds(crownPickupDelay);
        canPickupCrown = true;
    }

    private void dropCrown(GameObject player)
    {
        
        if (player.GetComponent<PlayerControl>().hasCrown)
        { 
            StartCoroutine(Stun());
            // set delay so player can't pick the crown right back up
            crown.GetComponent<Rigidbody>().isKinematic = false;
            crown.GetComponent<Rigidbody>().detectCollisions = true;
            // turn rigidbody back on
            crown.transform.SetParent(null);
            crown.GetComponent<Rigidbody>().AddForce(-Vector3.forward *
                                    crownDropForce, ForceMode.Impulse);
            // drop the crown
            CrownBehaviour.pickedUp = false;
            player.GetComponent<PlayerControl>().hasCrown = false;
        }
    }
}

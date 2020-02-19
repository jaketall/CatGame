using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Controller : MonoBehaviour
{
    /* DON'T USE THIS CLASS!!!!! */
    public float xClamp;
    public float yClamp;
    public float dashForce;
    public float thrust;
    private Rigidbody rb;
    public bool hasCrown;
    public GameObject crown;
    public float crownDropForce = 100;
    public float crownPickupDelay = 0.3f;
    public bool canPickupCrown;


    private Animator catAnim;
    private int runHash = Animator.StringToHash("Run");
    private int dashHash = Animator.StringToHash("Dash");
    private int wasHitHash = Animator.StringToHash("Was_Hit");

    private bool isDashing;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        catAnim = GetComponent<Animator>();
        canPickupCrown = true;
    }

    private void FixedUpdate()
    {
        float horizontal_input;
        float vertical_input;
        catAnim.SetBool(dashHash, false);

        if (Input.GetKey(KeyCode.J))
        {
            horizontal_input = -1;
        }
        else if (Input.GetKey(KeyCode.L))
        {
            horizontal_input = 1;
        }
        else
        {
            horizontal_input = 0;
        }

        if (Input.GetKey(KeyCode.I))
        {
            vertical_input = 1;
        }

        else if (Input.GetKey(KeyCode.K))
        {
            vertical_input = -1;
        }

        else
        {
            vertical_input = 0;
        }
        Vector3 newPos = new Vector3(horizontal_input, 0, vertical_input) * thrust;
        if (Input.GetKeyDown(KeyCode.Return))
        {
            dropCrown(gameObject);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!isDashing)
            {
                //dash
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
                //transform.rotation = Quaternion.LookRotation(newPos);
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
            crown.GetComponent<Rigidbody>().isKinematic = false;
            crown.GetComponent<Rigidbody>().detectCollisions = true;
            crown.transform.SetParent(null);
            crown.GetComponent<Rigidbody>().AddForce(-Vector3.forward *
                                    crownDropForce, ForceMode.Impulse);
            CrownBehaviour.pickedUp = false;
            player.GetComponent<PlayerControl>().hasCrown = false;
        }
    }
}
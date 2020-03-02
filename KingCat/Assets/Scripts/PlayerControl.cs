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
    public float currentScore = 0.0f;
    public int roundsWon = 0;

    public GameObject crown;
    public float crownDropForce = 100; //how far the crown goes when dropped
    public float crownPickupDelay = 0.3f;
    public bool canPickupCrown; //used in the "stun" function


    private Animator catAnim;
    private int runHash = Animator.StringToHash("Run");
    private int dashHash = Animator.StringToHash("Dash");
    private int wasHitHash = Animator.StringToHash("Was_Hit");

    public bool isDashing;
    public bool isStunned;
    public float dashForce;

    public string axis_v;
    public string axis_h;
    public string dash_str;
    public ParticleSystem dashParticle;
    public ParticleSystem stunnedParticle;

    public PowerController.Powers powers;

    // Start is called before the first frame update
    void Start()
    {
        powers = this.gameObject.GetComponent<PowerController>().powers;
        rb = GetComponent<Rigidbody>();
        catAnim = GetComponent<Animator>();
        canPickupCrown = true;
        String[] joysticks = Input.GetJoystickNames();
        Debug.Log("There are " + joysticks.Length + " controller(s) connected");
        for (int i = 0; i < joysticks.Length; i++){
            Debug.Log("Joystick" +joysticks[i]);
        }
    }

    private void FixedUpdate()
    {
        float horizontal_input = Input.GetAxis(axis_h);
        float vertical_input = Input.GetAxis(axis_v);
        Vector3 newPos;
        if(powers.speedBoost)
            newPos = new Vector3(horizontal_input, 0, vertical_input) * thrust * (1+powers.speedBoostPercent/100);
        else
            newPos = new Vector3(horizontal_input, 0, vertical_input) * thrust;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            //used for testing, can remove when inputs are working
            dropCrown(gameObject);
        }
        if (Input.GetButtonDown(dash_str))
        {
            if (!isDashing && !isStunned)
            {
                //dash
                //later we can set a delay for dashing
                if(dashParticle != null)
                {
                    dashParticle.Play(false);
                }
                catAnim.SetBool(dashHash, true);
                isDashing = true;
                StartCoroutine(Dash());

            }
        }
        else if (!newPos.Equals(Vector3.zero))
        {
            // run
            if (!isDashing && !isStunned)
            {
                rb.velocity = Vector3.zero;
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
        if(hasCrown)
        {
            currentScore += Time.deltaTime;
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
            if (isDashing && isLookingAt(collision.gameObject))
            {
                collision.gameObject.GetComponent<Animator>().SetTrigger(
                    wasHitHash);
                Debug.Log("stunned particle is" + stunnedParticle);
                collision.gameObject.GetComponent<PlayerControl>().stunnedParticle.Play();
                dropCrown(collision.gameObject);
            }
        }
    }
    private bool isLookingAt(GameObject player)
    {
        RaycastHit hit;
        if (Physics.SphereCast(new Ray(transform.position, transform.TransformDirection(Vector3.forward)), 2.5f, out hit))
        {

            if(hit.transform.gameObject.tag == "Player")
            {
                return true;
            }
        }
        return false;
    }
    IEnumerator Dash()
    {
        yield return new WaitForSeconds(0.3f);
        rb.AddRelativeForce(Vector3.forward * dashForce, ForceMode.Impulse);
        yield return new WaitForSeconds(0.3f);
        rb.velocity = Vector3.zero;
        isDashing = false;

    }
    IEnumerator Stun(GameObject player)
    {
        //delay pickup, so you can't pick it right back up after being stunned
        yield return new WaitForSeconds(crownPickupDelay);
        player.GetComponent<PlayerControl>().canPickupCrown = true;
    }

    public void dropCrown(GameObject player)
    {
        Debug.Log("drop crown called. has crown" + player.GetComponent<PlayerControl>().hasCrown);
        if (player.GetComponent<PlayerControl>().hasCrown)
        {
            
            // set delay so player can't pick the crown right back up
            crown.GetComponent<Rigidbody>().isKinematic = false;
            crown.GetComponent<Rigidbody>().detectCollisions = true;
            // turn rigidbody back on
            Debug.Log(crown.transform.parent);
            crown.transform.SetParent(null);
            StartCoroutine(Stun(player));
            Debug.Log(crown.transform.parent);
            crown.GetComponent<Rigidbody>().AddForce(Vector3.forward *
                crownDropForce, ForceMode.Impulse);
            // drop the crown
            CrownBehaviour.pickedUp = false;
            player.GetComponent<PlayerControl>().hasCrown = false;
            
        }
    }

    public void UpdateCurrentScore()
    {
        if (hasCrown)
        {
            currentScore += (int)Time.deltaTime * 1;
        }
    }
}

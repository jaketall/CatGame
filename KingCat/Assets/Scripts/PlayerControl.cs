using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using InControl;

public class PlayerControl : MonoBehaviour
{
    public float winHandicap;
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
    private int swipeHash = Animator.StringToHash("Swipe");
    private int wasHitHash = Animator.StringToHash("Was_Hit");

    public bool isDashing;
    public bool isStunned;
    public bool isSwiping;
    public bool isBalling;
    public float dashForce;

    public GameObject hairBall;
    public float throwForce = 10f;
    
    public AudioSource catAudio;
    public AudioClip dashSound;
    public AudioClip stunSound;
    public AudioClip swipeSound;

    public string axis_v;
    public string axis_h;
    public string dash_str;
    public string swipe_str;
    public string ball_str;
    public ParticleSystem dashParticle;
    public ParticleSystem stunnedParticle;
    public InputDevice joystick;
    public int controllerIndex;
    public PowerController.Powers powers;

    public KeyCode up;
    public KeyCode down;
    public KeyCode right;
    public KeyCode left;
    public KeyCode dashKey;
    public KeyCode swipeKey;
    public KeyCode ballKey;

    public int cat;
    /* 0 for white, 1 for blue, 2 for yellow, 3 for green */

    // Start is called before the first frame update
    void Start()
    {
        powers = this.gameObject.GetComponent<PowerController>().powers;
        rb = GetComponent<Rigidbody>();
        catAnim = GetComponent<Animator>();
        canPickupCrown = true;
        String[] joysticks = Input.GetJoystickNames();
        Debug.Log("There are " + joysticks.Length + " controller(s) connected");
        for (int i = 0; i < joysticks.Length; i++)
        {
            Debug.Log("Joystick " + joysticks[i]);
        }
        catAudio = GetComponent<AudioSource>();
        assignKeyCodes();
        switch (cat)
        {
            case 0:
                controllerIndex = CatIndex.whiteCatIndex;
                break;
            case 1:
                controllerIndex = CatIndex.blueCatIndex;
                break;
            case 2:
                controllerIndex = CatIndex.yellowCatIndex;
                break;
            case 3:
                controllerIndex = CatIndex.greenCatIndex;
                break;
        }
    }



    private void FixedUpdate()
    { 
        if (controllerIndex != -1)
        {
            joystick = InputManager.Devices[controllerIndex %
                                    InputManager.Devices.Count];
        }
        else
        {
            joystick = null;
        }
        float horizontal_input = 0;
        float vertical_input = 0;
        if (joystick == null)
        {
            if (Input.GetKey(up))
            {
                vertical_input = 1;
            }
            else if (Input.GetKey(down))
            {
                vertical_input = -1;
            }
            if (Input.GetKey(left))
            {
                horizontal_input = -1;
            }
            else if (Input.GetKey(right))
            {
                horizontal_input = 1;
            }
        }
        else
        {
            horizontal_input = joystick.LeftStickX;
            vertical_input = joystick.LeftStickY;
        }
        /*if ((joystick != null) && joystick.CommandWasPressed)
        {
            GameObject gameman = GameObject.Find("Game Manager");
            if (GameManager.isPaused)
            {
                gameman.GetComponent<GameManager>().Resume();
                GameManager.isPaused = false;
            }
            else
            {
                gameman.GetComponent<GameManager>().Pause();
                GameManager.isPaused = true;
            }
        }*/
        Vector3 newPos;
        if (powers.speedBoost)
            newPos = new Vector3(horizontal_input, 0, vertical_input) * thrust *(1-winHandicap*roundsWon)* (1 + powers.speedBoostPercent / 100);
        else
            newPos = new Vector3(horizontal_input, 0, vertical_input) * thrust *(1-winHandicap*roundsWon);

        if (Input.GetKeyDown(KeyCode.Return))
        {
            //used for testing, can remove when inputs are working
            dropCrown(gameObject);
        }
        if (Input.GetKeyDown(dashKey) || ((joystick != null) &&
                    joystick.Action1.WasPressed))
        {

            if (!isDashing && !isStunned)
            {
                //dash
                //later we can set a delay for dashing
                if (dashParticle != null)
                {
                    dashParticle.Play(false);
                }

                catAnim.SetBool(dashHash, true);
                catAudio.PlayOneShot(dashSound, 1.0f);
                isDashing = true;
                StartCoroutine(Dash());

            }
        }
        else if (Input.GetKeyDown(swipeKey) || ((joystick != null) &&
                            joystick.Action2.WasPressed))
        {
            if (!isSwiping && !isStunned)
            {
                catAnim.SetBool(swipeHash, true);
                catAudio.PlayOneShot(swipeSound, 1.0f);
                isSwiping = true;
                StartCoroutine(Swipe());
            }
        }
        else if (Input.GetKeyDown(ballKey)  || ((joystick != null) &&
                                   joystick.Action3.WasPressed))
        {
            if (!isBalling && !isStunned)
            {
                isBalling = true;
                StartCoroutine(Ball());
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
        UpdateCurrentScore();

    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("powerup"))
        {
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("HairBall"))
        {
            this.gameObject.GetComponent<PlayerControl>().setStun(powers.stunBoost, powers.stunBoostPercent);
            Destroy(other.gameObject);
        }
    }

    public void setStun(bool extraStun, float extraStunPercent)
    {
        if (powers.stunImmunity)
            return;

        Animator anim = this.gameObject.GetComponent<Animator>();
        anim.SetTrigger(wasHitHash);

        Debug.Log("stunned particle is" + stunnedParticle);

        anim.speed = 1; //set to default
        if (extraStun)
            anim.speed = (1 - extraStunPercent / 100);
        Debug.Log("Animation speed " + anim.speed);
        catAudio.PlayOneShot(stunSound, 0.4f);
        stunnedParticle.Play();
        dropCrown(this.gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if ((isDashing && isLookingAt(collision.gameObject)))
            {
                /*InputDevice controller = collision.gameObject.GetComponent<PlayerControl>().joystick;
                if (controller != null)
                    controller.Vibrate(100f);*/
                collision.gameObject.GetComponent<PlayerControl>().setStun(powers.stunBoost, powers.stunBoostPercent);
                //collision.gameObject.GetComponent<Animator>().SetTrigger(
                //    wasHitHash);
                //Debug.Log("stunned particle is" + stunnedParticle);
                //collision.gameObject.GetComponent<PlayerControl>().stunnedParticle.Play();
                //dropCrown(collision.gameObject);
            }
        }
    }
    private bool isLookingAt(GameObject player)
    {
        RaycastHit hit;
        if (Physics.SphereCast(new Ray(transform.position, transform.TransformDirection(Vector3.forward)), 2f, out hit))
        {

            if (hit.transform.gameObject.tag == "Player")
            {
                return true;
            }
        }
        else if (Physics.SphereCast(new Ray(transform.position, transform.TransformDirection(new Vector3(0.2f, 0, 1))), 2f, out hit))
        {

            if (hit.transform.gameObject.tag == "Player")
            {
                return true;
            }
        }
        else if (Physics.SphereCast(new Ray(transform.position, transform.TransformDirection(new Vector3(-0.2f, 0, 1))), 2f, out hit))
        {

            if (hit.transform.gameObject.tag == "Player")
            {
                return true;
            }
        }

        return false;
    }
    
    IEnumerator Ball()
    {
        
        Debug.Log("HairBall");
        GameObject hBall = Instantiate(hairBall, transform.position+(transform.forward*2), transform.rotation);
        hBall.GetComponent<MeshCollider>().isTrigger = false;
        Rigidbody rb = hBall.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
        yield return new WaitForSeconds(1f);
        hBall.GetComponent<MeshCollider>().isTrigger = true;
        yield return new WaitForSeconds(3f);
        isBalling = false;
        
    }

    IEnumerator Swipe()
    {
        
        int layerMask = 1 << 8;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            if (hit.distance < 10)
            {
                hit.transform.gameObject.GetComponent<PlayerControl>().setStun(powers.stunBoost, powers.stunBoostPercent);
                Debug.Log("HIT!");
            }
        }
        yield return new WaitForSeconds(0.5f);
        isSwiping = false;
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
        //player.GetComponent<PlayerControl>().joystick.StopVibration();
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
        if(!GameManager.roundOver)
        {
            if (hasCrown)
            {
                currentScore += Time.deltaTime;
                if (currentScore >= GameManager.maxScore)
                {
                    dropCrown(gameObject);
                    GameManager.EndRound(cat);
                }
            }
        }

    }

    private void assignKeyCodes()
    {
        switch (controllerIndex)
        {
            case 0:
                up = KeyCode.W;
                down = KeyCode.S;
                left = KeyCode.A;
                right = KeyCode.D;
                dashKey = KeyCode.Q;
                swipeKey = KeyCode.E;
                ballKey = KeyCode.X;
                break;
            case 1:
                up = KeyCode.T;
                down = KeyCode.G;
                left = KeyCode.F;
                right = KeyCode.H;
                dashKey = KeyCode.R;
                swipeKey = KeyCode.Y;
                ballKey = KeyCode.B;
                break;
            case 2:
                up = KeyCode.I;
                down = KeyCode.K;
                left = KeyCode.J;
                right = KeyCode.L;
                dashKey = KeyCode.U;
                swipeKey = KeyCode.O;
                ballKey = KeyCode.P;
                break;
            case 3:
                up = KeyCode.UpArrow;
                down = KeyCode.DownArrow;
                left = KeyCode.LeftArrow;
                right = KeyCode.RightArrow;
                dashKey = KeyCode.RightCommand;
                swipeKey = KeyCode.RightShift;
                ballKey = KeyCode.RightAlt;
                break;
            default:
                up = KeyCode.W;
                down = KeyCode.S;
                left = KeyCode.A;
                right = KeyCode.D;
                dashKey = KeyCode.Q;
                swipeKey = KeyCode.E;
                ballKey = KeyCode.X;
                break;
        }
    }


}

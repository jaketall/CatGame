using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speedBoostPercent;
    public float stunBoostPercent;
    public float hairballBoostPercent;


    [Serializable]
    public class PowerDuration
    {
        public float speedBoostDuration;
        public float stunBoostDuration;
        public float hairballBoostDuration;
        public float stunImmunityDuration;
        public float laserPointerDuration;
        public float bootsDuration;
    }
    public PowerDuration powerDurations = new PowerDuration();
    public class PowerTimer
    {
        public float speedBoostTimer;
        public float stunBoostTimer;
        public float hairballBoostTimer;
        public float stunImmunityTimer;
        public float laserPointerTimer;
        public float bootsTimer;
        private Powers powers;

        public PowerTimer(Powers p)
        {
            powers = p;
            speedBoostTimer=0;
            stunBoostTimer=0;
            hairballBoostTimer=0;
            stunImmunityTimer=0;
            laserPointerTimer=0;
            bootsTimer=0;
        }
        public void decrementPowerTimers()
        {
            Debug.Log(speedBoostTimer);

            if(speedBoostTimer>0)
            {
                speedBoostTimer -= Time.deltaTime;
                if(speedBoostTimer<=0)
                {
                    speedBoostTimer=0;
                    powers.speedBoost=false;
                }
            }
            if(stunBoostTimer>0)
            {
                stunBoostTimer -= Time.deltaTime;
                if(stunBoostTimer<=0)
                {
                    stunBoostTimer=0;
                    powers.stunBoost=false;
                }
            }
            if(hairballBoostTimer>0)
            {
                hairballBoostTimer -= Time.deltaTime;
                if(hairballBoostTimer<=0)
                {
                    hairballBoostTimer=0;
                    powers.hairballBoost=false;
                }
            }
            if(stunImmunityTimer>0)
            {
                stunImmunityTimer -= Time.deltaTime;
                if(stunImmunityTimer<=0)
                {
                    stunImmunityTimer=0;
                    powers.stunImmunity=false;
                }
            }
            if(laserPointerTimer>0)
            {
                laserPointerTimer -= Time.deltaTime;
                if(laserPointerTimer<=0)
                {
                    laserPointerTimer=0;
                    powers.laserPointer=false;
                }
            }
            if(bootsTimer>0)
            {
                bootsTimer -= Time.deltaTime;
                if(bootsTimer<=0)
                {
                    bootsTimer=0;
                    powers.boots=false;
                }
            }
        }
    }
    public class Powers
    {
        public bool speedBoost;
        public float speedBoostPercent;
        public float speedBoostDuration;

        public bool stunBoost;
        public float stunBoostPercent;
        public float stunBoostDuration;
        
        public bool hairballBoost;
        public float hairballBoostPercent;
        public float hairballBoostDuration;

        public bool stunImmunity;
        public float stunImmunityDuration;

        public bool whistle;

        public bool laserPointer;
        public float laserPointerDuration;

        public bool boots;
        public float bootsDuration;

        private PowerTimer timers;
        public Powers(
                    PowerDuration durations, 
                    float speedBoost_Percent, 
                    float stunBoost_Percent, 
                    float hairballBoost_Percent)
        {
            timers = new PowerTimer(this);
            speedBoost = false;
            hairballBoost = false;
            stunBoost = false;
            stunImmunity = false;
            whistle = false;
            laserPointer = false;
            boots = false;

            speedBoostDuration    = durations.speedBoostDuration;
            stunBoostDuration     = durations.stunBoostDuration;
            hairballBoostDuration = durations.hairballBoostDuration;
            stunImmunityDuration  = durations.stunImmunityDuration;
            laserPointerDuration  = durations.laserPointerDuration;
            bootsDuration         = durations.bootsDuration;

            speedBoostPercent = speedBoost_Percent;
            stunBoostPercent =  stunBoost_Percent;
            hairballBoostPercent = hairballBoost_Percent;

        }
        public void decrementPowerTimers()
        {
            timers.decrementPowerTimers();
        }
        public void setActive(string power)
        {
            var powerProperty = this.GetType().GetField(power);
            if(powerProperty == null)
            {
                Debug.Log("tried to set a power that doesn't exist: " + power);
                return;
            }
            else
            {
                powerProperty.SetValue(this, true);
            }

            var durationProperty= this.GetType().GetField(power+"Duration");
            var timerProperty = timers.GetType().GetField(power+"Timer");
            if(durationProperty != null && timerProperty != null)
                timerProperty.SetValue(timers, durationProperty.GetValue(this));
        }
    }
    public Powers powers;

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

    public bool isDashing;
    public bool isStunned;
    public float dashForce;

    public string axis_v;
    public string axis_h;
    public string dash_str;
    public ParticleSystem dashParticle;
    public ParticleSystem stunnedParticle;

    // Start is called before the first frame update
    void Start()
    {
        powers = new Powers(powerDurations, speedBoostPercent, stunBoostPercent, hairballBoostPercent);
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
        powers.decrementPowerTimers();

        float horizontal_input = Input.GetAxis(axis_h);
        float vertical_input = Input.GetAxis(axis_v);
        Vector3 newPos;
        if(powers.speedBoost)
            newPos = new Vector3(horizontal_input, 0, vertical_input) * thrust * (1+speedBoostPercent/100);
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
}

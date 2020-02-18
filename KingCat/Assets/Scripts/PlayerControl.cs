using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float xClamp;
    public float yClamp;
    public float dashForce;
    public float thrust; 
    private Rigidbody rb;

    public string axis_v;
    public string axis_h;
    
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
    }

    // Update is called once per frame
    void Update()
    {

        //rb.AddForce(thrust * horizontal_input * Vector3.right);
        //rb.AddForce(thrust * vertical_input * Vector3.forward);
        // transform.Translate(Time.deltaTime * horizontal_input * Vector3.right * speed);
        // transform.Translate(Time.deltaTime * vertical_input * Vector3.forward * speed);


    }
    private void FixedUpdate()
    {
        catAnim.SetBool(dashHash, false);
        float horizontal_input = Input.GetAxis(axis_h);
        float vertical_input = Input.GetAxis(axis_v);
        Vector3 newPos = new Vector3(horizontal_input, 0, vertical_input) * thrust;

        if (Input.GetKeyDown(KeyCode.Space))
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
            Debug.Log("is dashing? " + isDashing);
            if (isDashing)
            {
                collision.gameObject.GetComponent<Animator>().SetTrigger(
                    wasHitHash);
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
}

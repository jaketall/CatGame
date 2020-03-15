using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class SimplePlayerMovement : MonoBehaviour
{
	public int cat;
    private bool ready;
	private int controllerIndex;
	private InputDevice joystick;
	private Animator catAnim;
	private int runHash = Animator.StringToHash("Run");
	private int dashHash = Animator.StringToHash("Dash");
	private int swipeHash = Animator.StringToHash("Swipe");
	private int wasHitHash = Animator.StringToHash("Was_Hit");

	public bool isDashing;
	public bool isStunned;
	public bool isSwiping;
	public float dashForce;
    public float throwForce = 10f;

    private AudioSource catAudio;
	public AudioClip dashSound;
	public AudioClip stunSound;
	public AudioClip swipeSound;

    public GameObject hairBall;
    public bool isBalling;

    public ParticleSystem dashParticle;
	public ParticleSystem stunnedParticle;

	private Rigidbody rb;
    Vector3 originalPos;
    Quaternion originalRot;
	// Start is called before the first frame update
	void Start()
    {
		rb = GetComponent<Rigidbody>();
		catAnim = GetComponent<Animator>();
		catAudio = GetComponent<AudioSource>();
        originalPos = transform.position;
        originalRot = transform.rotation;
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        
		switch (cat)
		{
			case 0:
				controllerIndex = CatIndex.whiteCatIndex;
                ready = AssignCats.whiteCatisReady;
				break;
			case 1:
				controllerIndex = CatIndex.blueCatIndex;
                ready = AssignCats.blueCatisReady;
                break;
			case 2:
				controllerIndex = CatIndex.yellowCatIndex;
                ready = AssignCats.yellowCatisReady;
                break;
			case 3:
				controllerIndex = CatIndex.greenCatIndex;
                ready = AssignCats.greenCatisReady;
                break;
		}
        if (controllerIndex != -1 && !ready)
		{
			rb.isKinematic = false;
			joystick = InputManager.Devices[controllerIndex % InputManager.Devices.Count];
			float horizontal = joystick.LeftStickX;
			float vertical = joystick.LeftStickY;
			Vector3 newPos = new Vector3(-horizontal, 0, -vertical);
			if ((joystick != null) &&
			 joystick.Action1.WasPressed)
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
			else if ((joystick != null) &&
								joystick.Action2.WasPressed)
			{
				if (!isSwiping && !isStunned)
				{
					catAnim.SetBool(swipeHash, true);
					catAudio.PlayOneShot(swipeSound, 1.0f);
					isSwiping = true;
					StartCoroutine(Swipe());
				}
			}
            else if (((joystick != null) &&
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
		}
		else
		{
			rb.isKinematic = true;
            catAnim.SetBool(runHash, false);
            if (!transform.position.Equals(originalPos))
                rb.MovePosition(originalPos);
            if(!transform.rotation.Equals(originalRot))
                rb.MoveRotation(originalRot);
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
    IEnumerator Ball()
    {

        GameObject hBall = Instantiate(hairBall, transform.position + (transform.forward * 2), transform.rotation);
        hBall.GetComponent<MeshCollider>().isTrigger = false;
        Rigidbody rigb = hBall.GetComponent<Rigidbody>();
        rigb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
        yield return new WaitForSeconds(1f);
        hBall.GetComponent<MeshCollider>().isTrigger = true;
        yield return new WaitForSeconds(3f);
        isBalling = false;

    }
    public void setStun(bool extraStun, float extraStunPercent)
    {
        Animator anim = this.gameObject.GetComponent<Animator>();
        anim.SetTrigger(wasHitHash);
        anim.speed = 1; //set to default
        if (extraStun)
            anim.speed = (1 - extraStunPercent / 100);
        catAudio.PlayOneShot(stunSound, 0.4f);
        stunnedParticle.Play();
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

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			if ((isDashing && isLookingAt(collision.gameObject)) || (isSwiping && isLookingAt(collision.gameObject)))
			{
		
				collision.gameObject.GetComponent<SimplePlayerMovement>().setStun(false, 0);
			}
		}
	}
	IEnumerator Swipe()
	{
		yield return new WaitForSeconds(0.3f);
		isSwiping = false;
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("HairBall"))
        {
            this.gameObject.GetComponent<SimplePlayerMovement>().setStun(false, 0);
            Destroy(other.gameObject);
        }
    }
}

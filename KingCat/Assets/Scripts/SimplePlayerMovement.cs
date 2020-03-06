using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class SimplePlayerMovement : MonoBehaviour
{
	public int cat;
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

	private AudioSource catAudio;
	public AudioClip dashSound;
	public AudioClip stunSound;
	public AudioClip swipeSound;

	public ParticleSystem dashParticle;
	public ParticleSystem stunnedParticle;

	private Rigidbody rb;
	// Start is called before the first frame update
	void Start()
    {
		rb = GetComponent<Rigidbody>();
		catAnim = GetComponent<Animator>();
		catAudio = GetComponent<AudioSource>();
	}

    // Update is called once per frame
    void FixedUpdate()
    {
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
		if(controllerIndex != -1)
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
				catAudio.PlayOneShot(stunSound, 1.0f);
				collision.gameObject.GetComponent<Animator>().SetTrigger(
				    wasHitHash);
				collision.gameObject.GetComponent<PlayerControl>().stunnedParticle.Play();
			}
		}
	}
	IEnumerator Swipe()
	{
		yield return new WaitForSeconds(0.3f);
		isSwiping = false;
	}
}

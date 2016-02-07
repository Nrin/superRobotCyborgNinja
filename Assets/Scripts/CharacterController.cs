using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {

	[HideInInspector] public bool facingRight = true;
	[HideInInspector] public bool jump = false;
	public float defaultSpeed = 5f;
	public float sprintSpeed = 1.25f;
	public float maxSpeed = 10f;
	public float jumpForce = 1000f;
	public Transform rayA, rayB, rayC, rayD;

	private Rigidbody2D rb2d;
	private Transform charTransform;
	private bool grounded = false;
	private bool sprint = false;
	private float speed;


	void Start() {
		speed = defaultSpeed;
		rb2d = GetComponent<Rigidbody2D>();
		charTransform = GetComponent<Transform>();
	}

	void Update() {
		float h = Input.GetAxis("Horizontal");
		grounded = ExtendRays();
		speed = defaultSpeed;

		if (Input.GetButton("Sprint"))
			speed = sprintSpeed;

		if (h * rb2d.velocity.x < maxSpeed) {
			//rb2d.AddForce(Vector2.right * h * moveForce);
			transform.Translate(new Vector2(h * speed, 0.0f) * Time.deltaTime);
		}
		if (Mathf.Abs(rb2d.velocity.x) > maxSpeed) {
			//rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
			transform.Translate(new Vector2(h * speed, 0.0f) * Time.deltaTime);
		}

		if (h > 0 && !facingRight) 
			Flip();
		else if (h < 0 && facingRight)
			Flip();

		if (Input.GetButtonDown("Jump") && grounded)
			jump = true;
	}

	void FixedUpdate() {
		if (jump) {
			rb2d.AddForce(Vector2.up * jumpForce);
			jump = false;
		}
	}

	bool ExtendRays() {
		bool hit = 
		Physics2D.Raycast(rayA.position, Vector2.down, 0.1f, 1 << LayerMask.NameToLayer("Ground")) ||
		Physics2D.Raycast(rayB.position, Vector2.down, 0.1f, 1 << LayerMask.NameToLayer("Ground")) ||
		Physics2D.Raycast(rayC.position, Vector2.down, 0.1f, 1 << LayerMask.NameToLayer("Ground")) ||
		Physics2D.Raycast(rayD.position, Vector2.down, 0.1f, 1 << LayerMask.NameToLayer("Ground"));
		return hit;
	}

	//Flips the character about the y axis.
	void Flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}

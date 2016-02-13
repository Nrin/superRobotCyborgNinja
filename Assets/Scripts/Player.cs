using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Controller2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Player : MonoBehaviour {

	public float jumpHeight = 4;
	public float timeToJumpApex = 0.4f;
	public float accelerationTimeAirborn = 0.2f;
	public float accelerationTimeGrounded = 0.1f;
	public float walkSpeed = 1f;
	public float sprintSpeed = 1.25f;

	Vector3 velocity;
	float jumpVelocity;
	float gravity;
	float velocityXSmoothing;
	float moveSpeed;
	bool facingRight = true;

	Controller2D controller;
	SpriteRenderer sprite;
	
	void Start () {
		controller = GetComponent<Controller2D>();
		sprite = GetComponent<SpriteRenderer>();

		gravity = -(2 * jumpHeight) / Mathf.Pow (timeToJumpApex, 2);
		jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
		print("Gravity: " + gravity + ", Jump Velocity: " + jumpVelocity);
	}

	void Update() {
		moveSpeed = walkSpeed;
		if (Input.GetButton("Sprint"))
			moveSpeed = sprintSpeed;

		if (controller.collisions.above || controller.collisions.below) {
			velocity.y = 0;
		}

		Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

		if (input.x > 0 && !facingRight) {
			facingRight = !facingRight;
			sprite.flipX = false;
			//Flip();
		}
		else if (input.x < 0 && facingRight) {
			facingRight = !facingRight;
			sprite.flipX = true;
			//Flip();
		}

		if (Input.GetButtonDown("Jump") && controller.collisions.below) {
			velocity.y = jumpVelocity;
		}

		float targetVelocityX = input.x * moveSpeed;
		velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborn);
		velocity.y += (gravity * Time.deltaTime);
		controller.Move(velocity * Time.deltaTime);
	}



	//Alternate method of flipping the character
	void Flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
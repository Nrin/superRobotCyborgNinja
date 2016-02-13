using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Controller2D))]
public class SawBladeController : MonoBehaviour {
	
	public float moveSpeed = 5;
	public float gravity = -10;
	public bool left = true;
	public LayerMask groundMask;

	private float rayLength = 0.1f;
	Controller2D controller;
	Vector3 velocity;

	// Use this for initialization
	void Start () {
		controller = GetComponent<Controller2D>();
	}
	
	// Update is called once per frame
	void Update () {
		velocity.y += gravity * Time.deltaTime;

		if (controller.collisions.above || controller.collisions.below) {
			velocity.y = 0;
		}

		if (!left) 
			velocity.x = moveSpeed * Time.deltaTime;
		else if (left)
			velocity.x = moveSpeed * Time.deltaTime * -1;
		controller.raycastOrigins.bottomLeft += (Vector2.right * velocity.x);
		controller.raycastOrigins.bottomRight += (Vector2.right * velocity.x);
		Debug.DrawRay(controller.raycastOrigins.bottomLeft, Vector2.down, Color.red);
		Debug.DrawRay(controller.raycastOrigins.bottomRight, Vector2.down, Color.red);
		RaycastHit2D lHit = Physics2D.Raycast(controller.raycastOrigins.bottomLeft, Vector2.down, rayLength, groundMask);
		RaycastHit2D rHit = Physics2D.Raycast(controller.raycastOrigins.bottomRight, Vector2.down, rayLength, groundMask);
		if (controller.collisions.left || !lHit && velocity.y == 0)    
			left = false;
		if (controller.collisions.right || !rHit && velocity.y == 0)
			left = true;
		controller.Move(velocity);
	}

	//bool DetectEdge() {


		//return (!lHit || !rHit);
	//}
}

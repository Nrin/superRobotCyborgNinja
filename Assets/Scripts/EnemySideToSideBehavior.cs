using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EnemySideToSideBehavior : MonoBehaviour {

	public float speed = 1f;
	public Transform lgt, rgt, lRay, rRay, lWall, rWall;
	public LayerMask collisionMask;

	private Transform enemyTransform;
	private Rigidbody2D enemyRb2d;
	private bool leftEdge = false;
	private bool rightEdge = false;
	private bool leftWall = false;
	private bool rightWall = false;
	private bool left = true;

	// Use this for initialization
	void Start () {
		enemyTransform = GetComponent<Transform>();
		enemyRb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		SetCollisionTriggers();
		if (!leftEdge || leftWall)
			left = false;
		if (!rightEdge || rightWall)
			left = true;
		if (!left) {
			enemyTransform.Translate(new Vector2(speed * Time.deltaTime, 0));
		}
		else if (left) {
			enemyTransform.Translate(new Vector2(-speed * Time.deltaTime, 0));
		}
		leftEdge = false;
		rightEdge = false;
	}

	void SetCollisionTriggers() {
		leftWall = Physics2D.Linecast(lRay.position, lWall.position, collisionMask);
		rightWall = Physics2D.Linecast(rRay.position, rWall.position, collisionMask);
		leftEdge = Physics2D.Linecast(lRay.position, lgt.position, collisionMask);
		rightEdge = Physics2D.Linecast(rRay.position, rgt.position, collisionMask);
	}

	void DetectCollision() {
		if (!leftEdge || leftWall || !rightEdge || rightWall)
			left = !left;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag("Player"))
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
	}
}

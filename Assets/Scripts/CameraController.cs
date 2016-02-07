using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	
	public GameObject player;
	public GameObject cameraCalibrationPoint;
	public bool cameraFollowX = true;
	public bool cameraFollowY = true;
	public bool cameraFollowHeight = false;
	public float cameraHeight = -0.5f;

	private Transform cameraTransform;
	private Transform playerTransform;
	private Transform moveHeight;

	// Use this for initialization
	void Start () {
		cameraTransform = GetComponent<Transform>();
		playerTransform = player.GetComponent<Transform>();
		moveHeight = cameraCalibrationPoint.GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		//Locks the camera's vertical position.
		if (playerTransform.position.y < moveHeight.position.y)
			cameraTransform.position = new Vector3(playerTransform.position.x, moveHeight.position.y, -10f);
		else if (playerTransform.position.y >= 0.5f)
			cameraTransform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, -10f);

	}
}

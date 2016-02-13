using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelSelect : MonoBehaviour {

	public void LoadLevel0 () {
		if (Input.GetMouseButtonUp(0))
			SceneManager.LoadScene(0);
	}

	public void LoadLevel1 () {
		if (Input.GetMouseButtonUp(0))
			SceneManager.LoadScene(1);
	}

	public void LoadLevel2 () {
		if (Input.GetMouseButtonUp(0))
			SceneManager.LoadScene(2);
	}
}

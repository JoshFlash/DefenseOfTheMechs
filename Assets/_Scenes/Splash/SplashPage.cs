using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashPage : MonoBehaviour
{

	public float splashTime;

	void Start() {
		// Menu loads after 'splashTime' seconds of splash screen
		if (SceneManager.GetActiveScene().buildIndex == 0) { Invoke("LoadStartScene", splashTime); }

	}

	public void LoadStartScene() {
		SceneManager.LoadScene(1);
	}
}

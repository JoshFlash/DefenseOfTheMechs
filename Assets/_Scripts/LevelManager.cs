using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

	public void LoadLevelByIndex(int buildIndex) {
		if (buildIndex == 1) {
			ResetStaticVariables.Reset();
		}
		SceneManager.LoadScene(buildIndex);
	}

}

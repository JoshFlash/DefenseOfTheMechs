using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserController : MonoBehaviour {

	public static GameObject selectedTower;


	private void Update() {
		TestMethod();
	}

	void HandleActiveTower() {

	}


	void TestMethod() {
		if (Input.GetKeyDown(KeyCode.U)) {
			selectedTower.GetComponent<DefenseTower>().UpgradeBetaPath();
		}
		if (Input.GetKeyDown(KeyCode.Y)) {
			selectedTower.GetComponent<DefenseTower>().UpgradeAlphaPath();
		}
	}

}

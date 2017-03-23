using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserController : MonoBehaviour {

	public static GameObject selectedTower;

	private void Update() {
		TestMethod();
		SetSelectedTowerToNull();
	}

	void HandleActiveTower() {

	}

	void SetSelectedTowerToNull() {
		if (Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit)) {
				if (hit.collider.tag != "owned tower") {
					selectedTower = null;
				}
			}
		}
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

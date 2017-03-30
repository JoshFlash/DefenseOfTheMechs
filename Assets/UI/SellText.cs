using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SellText : MonoBehaviour {

	private Text sellText;

	void Start() {
		sellText = GetComponent<Text>();
	}

	void Update() {
		if (UserController.selectedTower)	sellText.text = "  Sell Value: \n    " + UserController.selectedTower.GetComponent<DefenseTower>().sellValue;
	}
}

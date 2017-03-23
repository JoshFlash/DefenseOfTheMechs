using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GoldCounter : MonoBehaviour {

	private Text goldText;

	void Start() {
		goldText = GetComponent<Text>();
	}

	void Update() {
		goldText.text = "RoboGold: " + MoneyManager.inLevelCash.ToString();
	}
}

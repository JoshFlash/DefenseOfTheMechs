using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GoldCounter : MonoBehaviour
{

	private Text goldText;

	void Start() {
		goldText = GetComponent<Text>();
	}

	void Update() {
		goldText.text = "  ɃotCoin: \n    " + MoneyManager.inLevelCash.ToString() + "\n Round:  " + (RoundManager.round-1);
	}
}

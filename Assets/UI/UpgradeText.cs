using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UpgradeText : MonoBehaviour
{

	public static Text upgradeText;

	private void Awake() {
		upgradeText = GetComponent<Text>();
	}

	public static void SetAlphaUpgradeText() {
		if (UserController.selectedTower) {
			DefenseTower _selectedTower = UserController.selectedTower.GetComponent<DefenseTower>();
			upgradeText.text = _selectedTower.AlphaUpgradeText(_selectedTower.alphaUpgradeLevel);
		}
	}

	public static void SetBetaUpgradeText() {
		if (UserController.selectedTower) {
			DefenseTower _selectedTower = UserController.selectedTower.GetComponent<DefenseTower>();
			upgradeText.text = _selectedTower.BetaUpgradeText(_selectedTower.betaUpgradeLevel);
		}
	}

}

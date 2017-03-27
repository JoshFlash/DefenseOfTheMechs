using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UserController : MonoBehaviour {

	public static GameObject selectedTower;

	public bool alphaButtonPressed, betaButtonPressed;
	private Text upgradeText;
	private bool doubleTimeOn = false;

	[SerializeField] private GameObject contextMenu;

	private void Awake() {
		Application.targetFrameRate = 80;
		contextMenu.SetActive(false);
	}

	private void Update() {
		SetSelectedTowerToNull();
	}

	public void HandleContextMenu() {
		if (selectedTower && alphaButtonPressed) {
			contextMenu.SetActive(true);
		} else if (selectedTower && betaButtonPressed) {
			contextMenu.SetActive(true);
		} else if (selectedTower) {
			contextMenu.SetActive(true); 
		} else {
			contextMenu.SetActive(false);
		}
	}

	public void PressAlphaButton() {
		betaButtonPressed = false;
		alphaButtonPressed = true;
		HandleContextMenu();
		UpgradeText.SetAlphaUpgradeText();
	}

	public void PressBetaButton() {
		alphaButtonPressed = false;
		betaButtonPressed = true;
		HandleContextMenu();
		UpgradeText.SetBetaUpgradeText();
	}

	public void PressBuyButton() {
		if		(alphaButtonPressed) {
			if (selectedTower.GetComponent<DefenseTower>().alphaUpgradeCost <= MoneyManager.inLevelCash) {
				selectedTower.GetComponent<DefenseTower>().UpgradeAlphaPath();
			}
		}
		else if (betaButtonPressed) {
			if (selectedTower.GetComponent<DefenseTower>().betaUpgradeCost <= MoneyManager.inLevelCash) {
				selectedTower.GetComponent<DefenseTower>().UpgradeBetaPath();
			}
		}
		contextMenu.SetActive(false);
	}

	void SetSelectedTowerToNull() {
		if (Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit)) {
				if (hit.collider.tag == "ground") {
					selectedTower = null;
					alphaButtonPressed = false;
					betaButtonPressed = false;
					HandleContextMenu();
				}
			}
		}
	}

	public void ToggleDoubleTime() {
		if		(!doubleTimeOn) {
			Time.timeScale = 1.9f;
		}
		else if (doubleTimeOn) {
			Time.timeScale = 1f;
		}
		doubleTimeOn = !doubleTimeOn;
	}

}

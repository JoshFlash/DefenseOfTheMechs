using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UserController : MonoBehaviour
{

	public bool dev_GodSpeed;
	public static GameObject selectedTower;

	public bool alphaButtonPressed, betaButtonPressed;
	private bool doubleTimeOn = false;

	[SerializeField] private GameObject contextMenu;
	[SerializeField] private GameObject towerMenu;
	[SerializeField] private GameObject upgradeMenu;


	private void Awake() {
		Application.targetFrameRate = 80;
		contextMenu.SetActive(false);
		towerMenu.SetActive(false);
		upgradeMenu.SetActive(false);
	}

	private void Update() {
		if (ClickedOnGround()) SelectNullTower();
		if (ClickedOnTower()) HandleContextMenu();
		ToggleQuadTime();
	}

	public void HandleContextMenu() {
		if (ClickedOnGround()) {
			contextMenu.SetActive(false);
		} else if (ClickedOnTower()) {
			upgradeMenu.SetActive(false);
			contextMenu = towerMenu;
			contextMenu.SetActive(true);
			PriorityText.SetPriorityText();
		} else {
			contextMenu.SetActive(true);
		}
	}

	public void PressAlphaButton() {
		if (selectedTower) {
			betaButtonPressed = false;
			alphaButtonPressed = true;
			towerMenu.SetActive(false);
			contextMenu = upgradeMenu;
			HandleContextMenu();
			UpgradeText.SetAlphaUpgradeText();
		}
	}

	public void PressBetaButton() {
		if (selectedTower) {
			alphaButtonPressed = false;
			betaButtonPressed = true;
			towerMenu.SetActive(false);
			contextMenu = upgradeMenu;
			HandleContextMenu();
			UpgradeText.SetBetaUpgradeText();
		}
	}

	public void PressBuyButton() {
		if		(alphaButtonPressed) {
			if (selectedTower.GetComponent<DefenseTower>().alphaUpgradeCost <= MoneyManager.inLevelCash) {
				selectedTower.GetComponent<DefenseTower>().UpgradeAlphaPath();
				UpgradeText.SetAlphaUpgradeText();
			}
		}
		else if (betaButtonPressed) {
			if (selectedTower.GetComponent<DefenseTower>().betaUpgradeCost <= MoneyManager.inLevelCash) {
				selectedTower.GetComponent<DefenseTower>().UpgradeBetaPath();
				UpgradeText.SetBetaUpgradeText();
			}
		}
	}

	public void PressSellButton() {
		MoneyManager.inLevelCash += selectedTower.GetComponent<DefenseTower>().sellValue;
		Destroy(selectedTower);
		contextMenu.SetActive(false);
	}

	bool ClickedOnGround() {
		if (Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit)) {
				if (hit.collider.tag == "ground") {
					return true;
				}
			}
		}
		return false;
	}

	bool ClickedOnTower() {
		if (Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit)) {
				if (hit.collider.tag == "owned tower") {
					return true;
				}
			}
		}
		return false;
	}

	void SelectNullTower() {
		selectedTower = null;
		towerMenu.SetActive(false);
		upgradeMenu.SetActive(false);
		alphaButtonPressed = false;
		betaButtonPressed = false;
		HandleContextMenu();
	}

	public void PressPriorityUpButton() {
		DefenseTower _selectedTower = selectedTower.GetComponent<DefenseTower>();
		_selectedTower.priorityIndex++;
		_selectedTower.ChangeTargetPriorty(_selectedTower.priorityIndex);
		PriorityText.SetPriorityText();
	}

	public void PressPiorityDownButton() {
		DefenseTower _selectedTower = selectedTower.GetComponent<DefenseTower>();
		if (_selectedTower.priorityIndex <= 0) _selectedTower.priorityIndex += 4;
		_selectedTower.priorityIndex--;
		_selectedTower.ChangeTargetPriorty(_selectedTower.priorityIndex);
		PriorityText.SetPriorityText();
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

	public void ToggleQuadTime() {
		if (dev_GodSpeed) {
			if (Input.GetKeyDown(KeyCode.LeftControl)) {
				Time.timeScale = 4.2f;
			}
			if (Input.GetKeyUp(KeyCode.LeftControl)) {
				Time.timeScale = 1f;
			}
		}
	}

}

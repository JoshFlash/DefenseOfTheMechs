using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PriorityText : MonoBehaviour
{
	public static Text priorityText;

	void Awake() {
		priorityText = GetComponent<Text>();
	}

	public static void SetPriorityText() {
		if (UserController.selectedTower) {
			DefenseTower _selectedTower = UserController.selectedTower.GetComponent<DefenseTower>();
			priorityText.text = _selectedTower.TargetPriorityText(_selectedTower.targetPriority);
		}
	}

}

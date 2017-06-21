using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetStaticVariables : MonoBehaviour 
{

	public static void Reset() {
		UserController.selectedTower = null;
		EnemySpawner.enemiesSpawned = 0;
		EscapedText.allowed = 20;
		EscapedText.escaped = 0;
	}
	
}

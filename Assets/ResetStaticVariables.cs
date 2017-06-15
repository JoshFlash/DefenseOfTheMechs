using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetStaticVariables : MonoBehaviour 
{

	public static void Reset() 
    {
		UserController.selectedTower = null;
		RoundManager.currentRound = 1;
		RoundManager.isRoundOver = true;
		EnemySpawner.enemiesSpawned = 0;
		EscapedText.allowed = 20;
		EscapedText.escaped = 0;

	}
	
}

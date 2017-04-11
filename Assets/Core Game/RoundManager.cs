using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// NOTES:
/// 
/// staggered waves will have shorter spawn intervals between distinct enemies
/// i.e. two staggered enemy types will have half of spawnInterval time 
/// between each distinct type but full spawnInterval between subsequent 
/// enemies of the same type
/// 
/// </summary>

public class RoundManager : MonoBehaviour
{

	public List<Round> allRounds;

	public static int currentRound;
	public static bool isRoundOver = true;

	[SerializeField] private EnemySpawner mainEnemySpawner;

	[SerializeField] private List<EnemySpawner> allEnemySpawners;

	private void Start() {
		currentRound = 1;
	}

	public void ResetRounds() {
		currentRound = 1;
	}

	public void StartRoundButton() {
		StartRound();
	}

	public void StartRound() {
		if (isRoundOver) {
			isRoundOver = false;
			StartCoroutine(ExecuteRound(allRounds[currentRound-1]));
			currentRound++;
		}
	}

	IEnumerator ExecuteRound(Round round) {
		for (int i = 0; i < round.totalEnemyWaves; i++) {
			Round.Wave wave = round.enemyWaves[i];
			wave.staggerInterval = wave.spawnInterval / wave.enemyTypes.Count; 
			yield return StartCoroutine(SpawnWave(wave));
		}
		yield return isRoundOver = true;
		MoneyManager.CollectInLevelCash(round.roundValue);
	}

	IEnumerator SpawnWave(Round.Wave wave) {
		for (int i = 1; i <= wave.enemyTypes.Count; i++) {
			if (i < wave.enemyTypes.Count) {
				StartCoroutine(mainEnemySpawner.SpawnEnemy(wave.enemyTypes[i-1], wave.enemiesInWave, wave.spawnInterval));
				yield return new WaitForSeconds(wave.staggerInterval);
			} else {
				yield return StartCoroutine(mainEnemySpawner.SpawnEnemy(wave.enemyTypes[i-1], wave.enemiesInWave, wave.spawnInterval));
			}
		}
		yield return new WaitForSeconds(wave.delayAfterWave);
	}


	// Below are the hard-coded rounds which are to be transfered to gameobjects via the editor


	IEnumerator Round1() {
		yield return StartCoroutine(mainEnemySpawner.SpawnEnemy(mainEnemySpawner.enemyTypes[2], 8, 4.2f));
		yield return new WaitForSeconds(3);
		yield return StartCoroutine(mainEnemySpawner.SpawnEnemy(mainEnemySpawner.enemyTypes[0], 4, 0.8f));
		yield return new WaitForSeconds(2);
		yield return StartCoroutine(mainEnemySpawner.SpawnEnemy(mainEnemySpawner.enemyTypes[0], 4, 0.6f));
		yield return isRoundOver = true;
		MoneyManager.CollectInLevelCash(24);
	}

	IEnumerator Round2() {
		yield return StartCoroutine(mainEnemySpawner.SpawnEnemy(mainEnemySpawner.enemyTypes[0], 10, 1.0f));
		yield return new WaitForSeconds(3);
		yield return StartCoroutine(mainEnemySpawner.SpawnEnemy(mainEnemySpawner.enemyTypes[1], 4, 0.8f));
		yield return new WaitForSeconds(2);
		yield return StartCoroutine(mainEnemySpawner.SpawnEnemy(mainEnemySpawner.enemyTypes[1], 2, 0.6f));
		yield return new WaitForSeconds(1.6f);
		yield return StartCoroutine(mainEnemySpawner.SpawnEnemy(mainEnemySpawner.enemyTypes[0], 2, 0.6f));
		yield return isRoundOver = true;
		MoneyManager.CollectInLevelCash(26);
	}

	IEnumerator Round3() {
		yield return StartCoroutine(mainEnemySpawner.SpawnEnemy(mainEnemySpawner.enemyTypes[0], 8, 0.8f));
		yield return new WaitForSeconds(3);
		yield return StartCoroutine(mainEnemySpawner.SpawnEnemy(mainEnemySpawner.enemyTypes[1], 8, 0.8f));
		yield return new WaitForSeconds(2);
		yield return StartCoroutine(mainEnemySpawner.SpawnEnemy(mainEnemySpawner.enemyTypes[1], 6, 0.6f));
		yield return new WaitForSeconds(1.6f);
		yield return StartCoroutine(mainEnemySpawner.SpawnEnemy(mainEnemySpawner.enemyTypes[0], 6, 0.6f));
		yield return isRoundOver = true;
		MoneyManager.CollectInLevelCash(30);
	}

	IEnumerator Round4() {
		yield return StartCoroutine(mainEnemySpawner.SpawnEnemy(mainEnemySpawner.enemyTypes[0], 6, 0.6f));
		yield return new WaitForSeconds(2);
					 StartCoroutine(mainEnemySpawner.SpawnEnemy(mainEnemySpawner.enemyTypes[0], 8, 0.8f));
		yield return new WaitForSeconds(0.4f);
		yield return StartCoroutine(mainEnemySpawner.SpawnEnemy(mainEnemySpawner.enemyTypes[1], 6, 0.8f));
		yield return new WaitForSeconds(2);
		yield return StartCoroutine(mainEnemySpawner.SpawnEnemy(mainEnemySpawner.enemyTypes[1], 6, 0.6f));
		yield return new WaitForSeconds(1.6f);
		yield return StartCoroutine(mainEnemySpawner.SpawnEnemy(mainEnemySpawner.enemyTypes[0], 6, 0.6f));
		yield return isRoundOver = true;
		MoneyManager.CollectInLevelCash(30);
	}

	IEnumerator Round5() {
		yield return StartCoroutine(mainEnemySpawner.SpawnEnemy(mainEnemySpawner.enemyTypes[1], 16, 0.6f));
		yield return new WaitForSeconds(2);
					 StartCoroutine(mainEnemySpawner.SpawnEnemy(mainEnemySpawner.enemyTypes[1], 8, 0.8f));
		yield return new WaitForSeconds(0.4f);
		yield return StartCoroutine(mainEnemySpawner.SpawnEnemy(mainEnemySpawner.enemyTypes[1], 8, 0.8f));
		yield return isRoundOver = true;
		MoneyManager.CollectInLevelCash(37);
	}

	IEnumerator Round6() {
		yield return StartCoroutine(mainEnemySpawner.SpawnEnemy(mainEnemySpawner.enemyTypes[2], 6, 1.1f));
		yield return new WaitForSeconds(1);
		yield return StartCoroutine(mainEnemySpawner.SpawnEnemy(mainEnemySpawner.enemyTypes[1], 8, 0.8f));
		yield return new WaitForSeconds(1);
		yield return StartCoroutine(mainEnemySpawner.SpawnEnemy(mainEnemySpawner.enemyTypes[1], 8, 0.6f));
		yield return new WaitForSeconds(1);
		yield return StartCoroutine(mainEnemySpawner.SpawnEnemy(mainEnemySpawner.enemyTypes[2], 6, 0.7f));
		yield return isRoundOver = true;
		MoneyManager.CollectInLevelCash(42);
	}

		IEnumerator DefaultRound() {
		yield return StartCoroutine(mainEnemySpawner.SpawnEnemy(mainEnemySpawner.enemyTypes[1], 10, 1.2f));
		yield return new WaitForSeconds(3);
		yield return StartCoroutine(mainEnemySpawner.SpawnEnemy(mainEnemySpawner.enemyTypes[1], 10, 0.8f));
		yield return new WaitForSeconds(3);
		yield return StartCoroutine(mainEnemySpawner.SpawnEnemy(mainEnemySpawner.enemyTypes[1], 6, 0.6f));
		yield return new WaitForSeconds(1.2f);
		yield return StartCoroutine(mainEnemySpawner.SpawnEnemy(mainEnemySpawner.enemyTypes[1], 8, 0.6f));
		yield return isRoundOver = true;
		MoneyManager.CollectInLevelCash(30);

	}
}


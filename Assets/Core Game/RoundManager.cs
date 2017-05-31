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
	public int dev_curRound;
	public List<Round> allRounds;

	public static int currentRound;
	public static bool isRoundOver = true;

	[SerializeField] private EnemySpawner mainEnemySpawner;

	[SerializeField] private List<EnemySpawner> allEnemySpawners;

	private void Start() {
		currentRound = dev_curRound;
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

}


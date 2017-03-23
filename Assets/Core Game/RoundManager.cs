using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour {

	public static int round;

	[SerializeField] private EnemySpawner mainEnemySpawner;

	[SerializeField] private EnemySpawner[] allEnemySpawners;

	private void Start() {
		StartRound(1);
	}


	public void ResetRounds() {
		round = 0;
	}

	public void StartRound(int round) {
		switch (round) {

			case 1:
				StartCoroutine(RoundOne());
				break;






		}
	}
	
	IEnumerator RoundOne() {
		yield return StartCoroutine(mainEnemySpawner.SpawnEnemy(mainEnemySpawner.basicEnemy, 10, 1.2f));
		yield return new WaitForSeconds(3);
		yield return StartCoroutine(mainEnemySpawner.SpawnEnemy(mainEnemySpawner.basicEnemy, 4, 0.8f));
		yield return new WaitForSeconds(2);
		yield return StartCoroutine(mainEnemySpawner.SpawnEnemy(mainEnemySpawner.basicEnemy, 2, 0.6f));
		yield return new WaitForSeconds(1.6f);
		yield return StartCoroutine(mainEnemySpawner.SpawnEnemy(mainEnemySpawner.basicEnemy, 2, 0.6f));
	}
}


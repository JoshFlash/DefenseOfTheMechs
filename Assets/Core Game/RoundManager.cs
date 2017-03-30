using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour {

	public static int round;
	public static bool isRoundOver = true;

	[SerializeField] private EnemySpawner mainEnemySpawner;

	[SerializeField] private List<EnemySpawner> allEnemySpawners;

	private void Start() {
		round = 1;
	}

	public void ResetRounds() {
		round = 1;
	}

	public void StartRoundButton() {
		StartRound(round);
	}

	public void StartRound(int _round) {
		if (isRoundOver) {
			isRoundOver = false;
			switch (_round) {

				case 1:
					StartCoroutine(Round1());
					round++;
					break;

				case 2:
					StartCoroutine(Round2());
					round++;
					break;

				case 3:
					StartCoroutine(Round3());
					round++;
					break;

				case 4:
					StartCoroutine(Round4());
					round++;
					break;

				case 5:
					StartCoroutine(Round5());
					round++;
					break;

				case 6:
					StartCoroutine(Round6());
					round++;
					break;




				default:
					StartCoroutine(DefaultRound());
					round++;
					break;					
			}

		}
	}

	IEnumerator Round1() {
		yield return StartCoroutine(mainEnemySpawner.SpawnEnemy(mainEnemySpawner.enemyTypes[0], 8, 1.2f));
		yield return new WaitForSeconds(3);
		yield return StartCoroutine(mainEnemySpawner.SpawnEnemy(mainEnemySpawner.enemyTypes[0], 4, 0.8f));
		yield return new WaitForSeconds(2);
		yield return StartCoroutine(mainEnemySpawner.SpawnEnemy(mainEnemySpawner.enemyTypes[0], 4, 0.6f));
		yield return isRoundOver = true;
		MoneyManager.CollectInLevelCash(24);
	}

	IEnumerator Round2() {
		yield return StartCoroutine(mainEnemySpawner.SpawnEnemy(mainEnemySpawner.enemyTypes[0], 10, 1.2f));
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
		yield return StartCoroutine(mainEnemySpawner.SpawnEnemy(mainEnemySpawner.enemyTypes[0], 10, 1.2f));
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


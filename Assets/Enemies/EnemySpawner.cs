using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

	public List<Enemy> serializedAllEnemies;

	public static List<Enemy> allEnemies;
	public static int enemiesSpawned;
	public List<Enemy> enemyTypes;

	private void Awake() {
		allEnemies = new List<Enemy>();
	}

	public IEnumerator SpawnEnemy(Enemy enemyType, int numberToSpawn, float spawnDelay) {
		for (int i = 1; i <= numberToSpawn; i++) {
			Enemy e = Instantiate(enemyType, this.transform.position, Quaternion.identity, this.transform);
			allEnemies.Add(e);
			enemiesSpawned++;
			yield return new WaitForSeconds(spawnDelay);
		}
	}

	public static void ClearNullEnemies() {
		if (allEnemies.Count > 0) {
			allEnemies.RemoveAll(item => item == null);
		}
	}

}

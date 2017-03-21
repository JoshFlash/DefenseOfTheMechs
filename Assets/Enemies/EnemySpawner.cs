using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	public static List<Enemy> allEnemies;
	public static int enemiesSpawned;
	public Enemy basicEnemy;

	private IEnumerator spawn;

	void Awake() {
		allEnemies = new List<Enemy>();
	}


	void Start() {
		spawn = SpawnEnemy(basicEnemy, 16, 0.6f);
		StartCoroutine(spawn);
	}

	private IEnumerator SpawnEnemy(Enemy enemyType, int numberToSpawn, float spawnDelay) {
		for (int i = 1; i <= numberToSpawn; i++) {
			Enemy e = Instantiate(enemyType, this.transform.position, Quaternion.identity, this.transform);
			allEnemies.Add(e);
			enemiesSpawned++;
			yield return new WaitForSeconds(spawnDelay);
		}
	}

	public static void ClearNullEnemies() {
		allEnemies.RemoveAll(item => item == null);
	}
}

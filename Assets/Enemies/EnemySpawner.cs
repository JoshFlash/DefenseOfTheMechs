using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public static List<Enemy> allEnemies;
	public static int enemiesSpawned;

	public List<Enemy> enemyTypes;

	void Awake() {
		allEnemies = new List<Enemy>();
	}

	void Start() {

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
		allEnemies.RemoveAll(item => item == null);
	}

}

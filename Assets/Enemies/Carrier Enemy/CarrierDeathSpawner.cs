using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrierDeathSpawner : MonoBehaviour
{

	public Enemy enemyTypeHeld;
	public int totalEnemiesHeld;
	public float spawnDelay;
	public int waypointNumber;
	public float spawnerLifetime;

	public float distanceTraversedByCarrier;

	void Start() {
		StartCoroutine(SpawnCarriedEnemy(enemyTypeHeld, totalEnemiesHeld, spawnDelay));
		Destroy(gameObject, spawnerLifetime);
	}

	public IEnumerator SpawnCarriedEnemy(Enemy enemyType, int numberToSpawn, float spawnDelay) {
		yield return new WaitForSeconds(0.7f);
		for (int i = 1; i <= numberToSpawn; i++) {
			Enemy e = Instantiate(enemyType, this.transform.position, Quaternion.identity);
			InitEnemy(e);
			EnemySpawner.allEnemies.Add(e);
			EnemySpawner.enemiesSpawned++;
			yield return new WaitForSeconds(spawnDelay);
		}
	}

	void InitEnemy(Enemy enemy) {
		enemy.distanceTraversed = distanceTraversedByCarrier;
		enemy.startingWaypointNumber = waypointNumber;
	}
}

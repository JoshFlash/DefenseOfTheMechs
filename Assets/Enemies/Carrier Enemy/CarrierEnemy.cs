using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CarrierEnemy : Enemy
{

	//public int enemiesHeld;
	//public float spawnRate;
	public GameObject deathSpawner;


	protected override void Update() {
		distanceTraversed += speed * Time.deltaTime;
		MoveSlowerAtLowHealth();
	}


	public override void Die() {
		GameObject spawner = Instantiate(deathSpawner, this.transform.position, Quaternion.identity);
		spawner.GetComponent<CarrierDeathSpawner>().waypointNumber = GetComponent<EnemyController>().pointNumber;
		spawner.GetComponent<CarrierDeathSpawner>().distanceTraversedByCarrier = GetComponent<Enemy>().distanceTraversed;
		Destroy(gameObject, 0.02f);
		EnemySpawner.enemiesSpawned--;
		MoneyManager.CollectInLevelCash(value);
	}


	private void MoveSlowerAtLowHealth() {

	}

}

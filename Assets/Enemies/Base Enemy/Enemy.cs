using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Enemy : MonoBehaviour
{

	public float distanceTraversed = 0f;
	public float speed;
	public float health;
	public int value;
	public int startingWaypointNumber = 0;
	public GameObject enemyDeathSparks;

	protected void Awake() {
		tag = "enemy";
	}

	protected void Start() {
		EnemyController ec = gameObject.AddComponent<EnemyController>();
		if (speed > 0) { ec.maxSpeed = speed; }
	}

	protected virtual void Update() {
		distanceTraversed += speed * Time.deltaTime;
	}

	protected void OnTriggerEnter2D(Collider2D collider) {

		Projectile proj = collider.gameObject.GetComponent<Projectile>();
		if (proj != null) {
			TakeDamage(proj.damage);
		}
	}

	public void TakeDamage(float damage) {
		health -= damage;
		if (health <= 0) {
			Die();
		}
	}

	public IEnumerator GetStunned(float stunTime) {
		speed = 0;
		EnemyController ec = gameObject.GetComponent<EnemyController>();
		ec.enabled = false;
		yield return new WaitForSeconds(stunTime);
		ec.enabled = true;
		speed = ec.maxSpeed;
	}

	public virtual void Die() {
		GameObject sparks = Instantiate(enemyDeathSparks, this.transform.position, Quaternion.identity);
		Destroy(sparks, 1f);
		Destroy(gameObject, 0.02f);
		EnemySpawner.enemiesSpawned--;
		MoneyManager.CollectInLevelCash(value);
	}

}

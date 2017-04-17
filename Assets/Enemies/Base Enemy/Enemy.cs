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
	public bool isPoisoned;
	public bool isSlowed;

	private int ticksSincePoison = 0;
	private IEnumerator poisoned;

	protected void Awake() {
		tag = "enemy";
		isPoisoned = false;
		isSlowed = false;
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
			if (proj.isPoisonous) {
				ticksSincePoison = 0;
				if (!isPoisoned) {
					poisoned = Poisoned(proj.poisonTicks, proj.poisonInterval, proj.poisonDamage);
					StartCoroutine(poisoned);
					if (proj.isSlowing) {
						StartCoroutine(GetSlowed(proj.poisonTicks * proj.poisonTicks, proj.slowAmount));
					}
				}
				else if (isPoisoned) {
					StopCoroutine(poisoned);
					poisoned = Poisoned(proj.poisonTicks, proj.poisonInterval, proj.poisonDamage);
					StartCoroutine(poisoned);
					if (proj.isSlowing) {
						StartCoroutine(GetSlowed(proj.poisonTicks * proj.poisonTicks, proj.slowAmount));
					}
				}
			}
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

	public IEnumerator GetSlowed(float slowTime, float slowAmount) {
		speed *= 1 - slowAmount;
		EnemyController ec = gameObject.GetComponent<EnemyController>();
		ec.maxSpeed = speed;
		yield return new WaitForSeconds(slowTime);
		speed /= 1 - slowAmount;
		ec.maxSpeed = speed;
	}

	public IEnumerator Poisoned(float poisonTicks, float poisonInterval, float posionDamage) {
		isPoisoned = true;
		while (ticksSincePoison < poisonTicks) {
			yield return new WaitForSeconds(poisonInterval);
			TakeDamage(posionDamage);
			ticksSincePoison++;
		}
		isPoisoned = false;
		ticksSincePoison = 0;
	}

	public virtual void Die() {
		GameObject sparks = Instantiate(enemyDeathSparks, this.transform.position, Quaternion.identity);
		Destroy(sparks, 1f);
		Destroy(gameObject, 0.02f);
		EnemySpawner.enemiesSpawned--;
		MoneyManager.CollectInLevelCash(value);
	}

}

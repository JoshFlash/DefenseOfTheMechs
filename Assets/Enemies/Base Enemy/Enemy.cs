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
	public GameObject poisonEffect;
	public bool isPoisoned;
	public bool isSlowed;
	public List<AudioClip> clips;

	private int ticksSincePoison = 0;
	private IEnumerator poisoned;

	protected void Awake() {
		tag = "enemy";
		isPoisoned = false;
		isSlowed = false;
	}

	protected virtual void Start() {
		EnemyController ec = gameObject.AddComponent<EnemyController>();
		if (speed > 0) { ec.maxSpeed = speed; }
	}

	protected virtual void Update() {
		distanceTraversed += speed * Time.deltaTime;
	}

	protected virtual void OnTriggerEnter2D(Collider2D collider) {

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
						StartCoroutine(GetSlowed(proj.poisonTicks * proj.poisonInterval, proj.slowAmount));
					}
				}
			}
		}
	}

	public void TakeDamage(float damage) {
		health -= damage;
		if (health <= 0) {
			AudioSource.PlayClipAtPoint(clips[1], transform.position);
			Die();
		}
		AudioSource.PlayClipAtPoint(clips[0], transform.position);

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

		GameObject poisonParticles = Instantiate(poisonEffect, this.transform.position, Quaternion.identity);
		poisonParticles.GetComponent<PoisonEffect>().parentEnemy = this.gameObject;
		Destroy(poisonParticles, poisonInterval * poisonTicks);

		GetComponent<SpriteRenderer>().color = new Color(0.7f, 1, 0.7f, 0.9f); 
		while (ticksSincePoison < poisonTicks) {
			yield return new WaitForSeconds(poisonInterval);
			TakeDamage(posionDamage);
			ticksSincePoison++;
		}
		GetComponent<SpriteRenderer>().color = new Color(1,1,1,1); 

		isPoisoned = false;
		ticksSincePoison = 0;
	}

	public virtual void Die() {
		GameObject sparks = Instantiate(enemyDeathSparks, this.transform.position, Quaternion.identity);
		Destroy(sparks, 1f);
		Destroy(gameObject, 0.02f);
		EnemySpawner.enemiesSpawned--;
		MoneyManager.CollectInLevelCash(value);
		value = 0;
	}

}

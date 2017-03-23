using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float distanceTraversed;
	public float speed;
	public float health;
	public int value;

	void Awake() {
		distanceTraversed = 0f;
		tag = "enemy";
	}

	void Start() {
		EnemyController ec = gameObject.AddComponent<EnemyController>();
		if (speed > 0) { ec.maxSpeed = speed; }
	}

	void Update() {
		distanceTraversed += speed * Time.deltaTime;
	}

	void OnTriggerEnter2D(Collider2D collider) {
		Projectile proj = collider.gameObject.GetComponent<Projectile>();
		if ( proj != null) {
			TakeDamage(proj.damage);
		}
	}

	void TakeDamage(float damage) {
		health -= damage;
		if (health <= 0) {
			Die();
		}
	}

	void Die() {
		//death animation anim.Play()
		float anim_length = 0.06f;
		Destroy(gameObject, anim_length);
		EnemySpawner.enemiesSpawned--;
		MoneyManager.CollectInLevelCash(value);
		Invoke("EnemySpawner.ClearNullEnemies", anim_length + Time.deltaTime);
	}

}

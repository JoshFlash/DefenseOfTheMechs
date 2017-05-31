using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

	public float damage, speed, range;
	public float extraRange;
	public int multiHit;

	public bool isPoisonous;
	public bool isSlowing;
	public float poisonDamage;
	public float poisonInterval;
	public float poisonTicks;
	public float slowAmount; // % between 0 and 1

	private Rigidbody2D rigidBody;
	private float distanceTraversed;

	void Awake() {
		distanceTraversed = 0;
		tag = "projectile";
		GetComponent<SpriteRenderer>().sortingLayerName = "projectiles";
	}

	private void Start() {

	}

	void Update() {
		distanceTraversed += speed * Time.deltaTime;
		if (distanceTraversed > range) Destroy(gameObject);
	}

	private void OnTriggerEnter2D(Collider2D collider) {
		if (collider.tag == "enemy" && this.tag == "projectile") {
			multiHit--;
			if (multiHit <= 0) {
				Destroy(gameObject);
			}
		}
	}

}

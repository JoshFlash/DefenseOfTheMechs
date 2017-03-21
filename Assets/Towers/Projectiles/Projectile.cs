using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public float damage, speed, range;
	public float extraRange;
	public int multiHit;

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
				Destroy(gameObject, 0.03f);
			}
		}
	}






	/* ------------ The below code was for Lazer Blazer ------------ **
				----		Left here for reference		----
	private float lazerSpeed;
	private float lazerLife;

	// Use this for initialization
	void Start() {
		damage = 150;
		//lazerSpeed = 5f;
		lazerLife = 3f;
	}

	// Update is called once per frame
	void Update() {
		//transform.position += new Vector3(0, lazerSpeed*Time.deltaTime, 0);
		Destroy(gameObject, lazerLife);
	}

	private void OnTriggerEnter2D(Collider2D collider) {
		if		(collider.tag == "enemy" && this.tag == "lazer")		{ Destroy(gameObject); } 
		if		(collider.tag == "player" && this.tag == "enemy_lazer")	{ Destroy(gameObject); }
	}

	public float GetDamage() {
		return damage;
	}*/


}

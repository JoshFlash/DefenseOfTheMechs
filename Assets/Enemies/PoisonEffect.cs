using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonEffect : MonoBehaviour 
{
	public GameObject parentEnemy;
	public float lerpSpeed;

	private void Update() {
		if (parentEnemy == null) Destroy(gameObject);
		else {
			transform.position = Vector3.Lerp(this.transform.position, parentEnemy.transform.position, lerpSpeed * Time.deltaTime);
			transform.up = Vector3.Slerp(this.transform.up, parentEnemy.transform.up, 1.1f * Time.deltaTime);
		}
	}

}

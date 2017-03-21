using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRadial : MonoBehaviour {

	float sightRadius;
	Vector3 originalScale;

	private void Awake() {
		originalScale = transform.localScale;
		sightRadius = GetComponentInParent<DefenseTower>().towerRange;
		transform.localScale *= 2*sightRadius;
	}

	public void UpgradeSight() {
		sightRadius = GetComponentInParent<DefenseTower>().towerRange;
		transform.localScale = originalScale * 2 * sightRadius;
	}

}

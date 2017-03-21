using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewTower : MonoBehaviour {

	private float cameraDistance;
	private bool towerIsPlaceable;
	private SpriteRenderer spriteRend;
	private Vector3 defaultPosition;

	[SerializeField] private int towerCost;
	[SerializeField] private float towerSphereCastRadius;
	[SerializeField] private Vector3 towerSphereCastOffset;
	[SerializeField] private Color placeableColor;
	[SerializeField] private Color unplaceableColor;
	[SerializeField] private GameObject towerTypePrefab;
	[SerializeField] private Camera _camera;

	void Awake() {
		towerIsPlaceable = false;
		tag = "new tower";
	}

	void Start() {
		spriteRend = GetComponent<SpriteRenderer>();
		cameraDistance = -_camera.transform.position.z;
		defaultPosition = transform.position;
	}

	private void OnMouseUp() {
		if (towerIsPlaceable && MoneyManager.inLevelCash >= towerCost ) {
			PlaceTower();
			MoneyManager.SpendInLevelCash(towerCost);
		} 

		else { ReturnToOrigin(); }
	}

	private void OnMouseDrag() {
		MoveTower();
		CheckIfTowerIsPlaceable();
		ColorTowerToShowIfPlaceable();
	}

	void MoveTower() {
		transform.position = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraDistance));
	}

	void CheckIfTowerIsPlaceable() {
		Ray ray = new Ray(transform.position + towerSphereCastOffset, transform.forward);
		RaycastHit hit;
		if (Physics.SphereCast(ray, towerSphereCastRadius, out hit)) {
			if (hit.collider.tag == "ground") {
				towerIsPlaceable = true;
			}

			else {
				towerIsPlaceable = false;
			}
		}
	}

	void ColorTowerToShowIfPlaceable() {
		if (towerIsPlaceable)	{ spriteRend.color = placeableColor; }
		if (!towerIsPlaceable)	{ spriteRend.color = unplaceableColor; }
	}

	void PlaceTower() {
		Instantiate(towerTypePrefab, transform.position, Quaternion.identity);

		ReturnToOrigin();
	}

	void ReturnToOrigin() {
		transform.position = defaultPosition;
		spriteRend.color = placeableColor;
	}

}

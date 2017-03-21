using System.Collections;
using System.Collections.Generic;
using Flash.Feather;
using UnityEngine;

public class Waypoint : MonoBehaviour {

	public int pointNumber;

	private void Awake() {
		pointNumber = Utilities.ConvertToInt(name);
	}

	void OnDrawGizmos() {
		Gizmos.color = new Color(.2f,.4f,.2f,.4f) ;
		Gizmos.DrawSphere(transform.position, 0.18f);
	}

}

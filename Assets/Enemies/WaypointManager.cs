using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Flash.Feather;

public class WaypointManager : MonoBehaviour {

	public static Waypoint[] levelWaypoints;

	private void Awake() {
		levelWaypoints = FindObjectsOfType<Waypoint>() as Waypoint[];
		Array.Sort(levelWaypoints, delegate (Waypoint x, Waypoint y) { return x.pointNumber.CompareTo(y.pointNumber); });
	}

	public void ClearLevelWaypoints() {
		Array.Clear(levelWaypoints,0,levelWaypoints.Length);
	}

}

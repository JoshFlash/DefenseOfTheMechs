using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour {

	public static int inLevelCash;  // this is the $ used to buy towers and upgrades within a level -  earned in-level, spent in-level - it resets upon starting a new level
	public int startCash;

	void Awake() {
		inLevelCash = startCash;
	}

	public static void SpendInLevelCash(int cost) {
		inLevelCash -= cost;
	}

	public static void CollectInLevelCash(int value) {
		inLevelCash += value;
	}

}

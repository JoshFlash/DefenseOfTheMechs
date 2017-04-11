using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Round : MonoBehaviour 
{
	[System.Serializable]
	public class Wave
	{
		public List<Enemy> enemyTypes;
		public int enemiesInWave;
		public float spawnInterval;
		[HideInInspector] public float staggerInterval;
		public float delayAfterWave;
	}

	public int roundValue;
	public int totalEnemyWaves;
	public bool lockWaveCount = false;

	public List<Wave> enemyWaves;

	private void Update() {

		if (!lockWaveCount) {

			if (enemyWaves.Count > totalEnemyWaves) {
				int difference = enemyWaves.Count - totalEnemyWaves;
				for (int i = difference; i > 0; i--) {
					enemyWaves.Remove(enemyWaves[enemyWaves.Count - i]);
				}
			}

			if (enemyWaves.Count < totalEnemyWaves) {
				int difference = totalEnemyWaves - enemyWaves.Count;
				for (int i = difference; i > 0; i--) {
					Wave wave = new Wave();
					enemyWaves.Add(wave);
				}
			}

		}

	}


}

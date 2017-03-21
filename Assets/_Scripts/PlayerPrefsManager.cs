using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour {

	const string MUSIC_VOLUME_KEY = "music_volume";
	const string SFX_VOLUME_KEY = "sfx_volume";
	const string LEVEL_KEY = "level_unlocked_";
	const string DIFFICULTY_KEY = "difficulty";


	public static void SetMusicVolume(float volume) {
		if (volume >= 0 && volume <= 1) {
			PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, volume);
		} else { Debug.LogError("Volume out of range (music)"); }
	}

	public static float GetMusicVolume() {
		return PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY);
	}

	public static void SetSfxVolume(float volume) {
		if (volume >= 0 && volume <= 1) {
			PlayerPrefs.SetFloat(SFX_VOLUME_KEY, volume);
		} else { Debug.LogError("Volume out of range (sfx)"); }
	}

	public static float GeSfxVolume() {
		return PlayerPrefs.GetFloat(SFX_VOLUME_KEY);
	}

	public static void UnlockLevel (int level) {
		//code later
	}

	public static bool IsLevelUnlocked(int level) {
		return true;
		//code later
	}

	public static void SetDifficulty(float difficulty) {
		if (difficulty >= 0 && difficulty <= 3) {
			PlayerPrefs.SetFloat(DIFFICULTY_KEY, difficulty);
		} else { Debug.LogError("Difficulty out of range"); }
	}

	public static float GetDifficulty() {
		return PlayerPrefs.GetFloat(DIFFICULTY_KEY);
	}


}

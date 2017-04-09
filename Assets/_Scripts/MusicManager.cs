using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MusicManager : MonoBehaviour
{

	public AudioClip[] levelMusic;

	private AudioSource audioSource;
	private int levelIndex;

	private void Awake() {
		audioSource = GetComponent<AudioSource>();
		levelIndex = SceneManager.GetActiveScene().buildIndex;
	}

	void Start() {
		ChangeMusic();
	}

	void ChangeMusic() {
		audioSource.Stop();
		audioSource.clip = levelMusic[levelIndex];
		audioSource.PlayDelayed(0.5f);
	}

	public void SetVolume(float volume) {
		audioSource.volume = volume;
	}
}

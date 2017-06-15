using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLoseManager : MonoBehaviour 
{

	private bool winMessageReceived = false;
	private bool loseMessageReceived = false;

	[SerializeField] GameObject winPanel;
	[SerializeField] GameObject losePanel;
	[SerializeField] GameObject homeButton;


	public void Win() 
    {
		if (!winMessageReceived && !loseMessageReceived) {
			winMessageReceived = true;
			RunWinSequence();
		}
	}
	public void Lose()
	{
		if (!loseMessageReceived) {
			loseMessageReceived = true;
			RunLoseSequence();
		}
	}

	private void RunWinSequence()
	{
		winPanel.SetActive(true);
		winPanel.transform.position = new Vector3(0, 0, -1);
		GetComponent<AudioSource>().Play();
		homeButton.SetActive(true);
	}
	private void RunLoseSequence()
	{
		losePanel.SetActive(true);
		losePanel.transform.position = new Vector3(0, 0, -1);
		homeButton.SetActive(true);
	}
}

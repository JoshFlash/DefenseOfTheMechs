using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EscapedText : MonoBehaviour
{
	public WinLoseManager LOSEMANAGER;
	public Text text;
	public static int allowed = 20;
	public static int escaped = 0;

	void Update () {
		text.text = "ESCAPED ENEMIES: " + escaped + "/" + allowed;
		if (allowed <= escaped) {
			SendLoseMessage();
		}
	}

	public static void AddToEscapedNumber() {
		escaped++;
	}

	public void SendLoseMessage()
	{
		LOSEMANAGER.Lose();
	}

}

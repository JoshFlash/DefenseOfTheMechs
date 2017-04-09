using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EscapedText : MonoBehaviour
{

	public Text text;
	public static int escaped = 0;

	void Update () {
		text.text = "Escaped Enemies " + escaped;
	}

	public static void AddToEscapedNumber() {
		escaped++;
	}
}

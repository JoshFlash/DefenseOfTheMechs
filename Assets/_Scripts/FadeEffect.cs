using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FadeEffect : MonoBehaviour {

	public bool fadeOut;
	public float fadeTime, fadeDelay;

	private Image fadeOverlay;

	void Awake () {
		fadeOverlay = GetComponent<Image>();
		if		(fadeOut)	{ fadeOverlay.color = new Color(0, 0, 0, 0); } 
		else if	(!fadeOut)	{ fadeOverlay.color = new Color(0, 0, 0, 1); }
	}

	void Update () {

		if (	(Time.timeSinceLevelLoad < fadeTime + fadeDelay)
		&&		(Time.timeSinceLevelLoad > fadeDelay)	)			{ Fade(); } 

		if (	(Time.timeSinceLevelLoad > fadeTime + fadeDelay)
		&&		(!fadeOut)	)										{ Destroy(gameObject); }

	}

	void Fade() {
		float alphaChange = Time.deltaTime / fadeTime;
		if		(fadeOut)	{ fadeOverlay.color += new Color(0, 0, 0, alphaChange); } 
		else if (!fadeOut)	{ fadeOverlay.color -= new Color(0, 0, 0, alphaChange); }
	}


}

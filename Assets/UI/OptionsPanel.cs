using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsPanel : MonoBehaviour
{

	public GameObject optionsPanel;

	private Animation anim;

	void Start() {
		anim = GetComponent<Animation>();
	}

	public void CallOptionsPanel() {
		optionsPanel.SetActive(true);
	}

	public void CloseOptionsPanel() {
		anim.Play("PanelSlideUp");
		Invoke("SetOptionsInactive", .8f);
	}

	void SetOptionsInactive() {
		optionsPanel.SetActive(false);
	}	
}

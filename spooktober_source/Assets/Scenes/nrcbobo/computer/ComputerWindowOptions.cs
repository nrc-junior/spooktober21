using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerWindowOptions : MonoBehaviour {
	[HideInInspector] public Minigame minigame;
	GameObject disabled_desktop;
	
	public void SetMinigame(Minigame mg, GameObject dd) {
		disabled_desktop = dd;
		minigame = mg;
	}

	public void endSession() {
		disabled_desktop.SetActive(true);
		minigame.gameObject.SetActive(false);
		gameObject.SetActive(false);
	}
}

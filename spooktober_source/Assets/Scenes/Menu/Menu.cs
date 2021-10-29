using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Menu : MonoBehaviour {
	public GameObject settings;
	bool using_settings;

	void Start() {
		
	}
	public void CallStart() {
		
	}

	public void CallSetings() {
		if (using_settings) {
			settings.SetActive(false);
			using_settings = false;
		}else {
			settings.SetActive(true);
			using_settings = true;
		}
	}
	
	public void CallQuit() {
		Application.Quit();
	}
	
}

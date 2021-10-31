using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour {
	public GameObject settings;
	bool using_settings;

	void Continue() {
		StaticDataLoader.event_minigame1_finished = PlayerPrefs.GetInt("Minigame1", -1) == 1;
		StaticDataLoader.event_minigame2_finished = PlayerPrefs.GetInt("Minigame2", -1) == 1;
		CallStart();
	}
	
	public void CallStart() {
		SceneManager.LoadScene(1);
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

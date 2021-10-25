using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerApp : MonoBehaviour {
    public Minigame mg; 
    Transform parent;
    ComputerDesktop desktop;
    
    void Start() {
	    desktop = (parent = transform.parent).GetComponent<ComputerDesktop>();
    }

    
    public void LoadGame() {
	    if (mg.completed) {
		    desktop.Crash(mg.app_name);
			return;
	    }
		
	    parent.gameObject.SetActive(false);
	    desktop.StartGame(mg);
	    mg.Launch();
    }
}

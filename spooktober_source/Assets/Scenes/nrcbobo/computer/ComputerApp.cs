using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerApp : MonoBehaviour {
    Transform parent;
    ComputerDesktop desktop;
    
    void Start() {
	    desktop = (parent = transform.parent).GetComponent<ComputerDesktop>();
    }

    
    public void LoadGame(Minigame mg) {
	    
		if (!desktop.connection) {
		    desktop.noConnection("DOWNLOADING...");
	    }else {
			desktop.noConnection("DOWNLOADING...", true, mg.id);
		}
		
	    //parent.gameObject.SetActive(false);
	    //desktop.StartGame(mg);
	    //mg.Launch();
    }
}

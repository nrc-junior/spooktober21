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
	    
	    if (mg.completed) {
		    desktop.Crash(mg.app_name);
			return;
	    }else if (!desktop.connection) {
		    desktop.noConnection();
	    }
		
	    //parent.gameObject.SetActive(false);
	    //desktop.StartGame(mg);
	    //mg.Launch();
    }
}

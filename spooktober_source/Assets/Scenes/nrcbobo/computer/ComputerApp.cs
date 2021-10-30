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
			string message = "DOWNLOADING...";
			switch (mg.secondary_id){
				case 2: message = StaticDataLoader.event_minigame1_finished ? "OPENING..." : message; 
					break;
				case 3: message = StaticDataLoader.event_minigame2_finished ? "OPENING..." : message; 
					break;
				case 4: message = StaticDataLoader.event_minigame3_finished ? "OPENING..." : message; 
					break;
			}
			desktop.noConnection(message, true, mg.id);
		}
		
	    //parent.gameObject.SetActive(false);
	    //desktop.StartGame(mg);
	    //mg.Launch();
    }
}

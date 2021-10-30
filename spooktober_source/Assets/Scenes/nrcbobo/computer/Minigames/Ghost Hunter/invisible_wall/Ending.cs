using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour {
	public bool bad_ending;
	public Animator door;
	
	void OnTriggerEnter(Collider col) {
		if (col.tag == "Player") {
			var p = col.GetComponent<PlayerCombatSystem>();
			
			if (p.has_secret_key && bad_ending) {
				print("conquistou bad ending");
			}

			if (!p.has_secret_key && !bad_ending){
				print("conquistou good ending");
			}
		}
	}
}

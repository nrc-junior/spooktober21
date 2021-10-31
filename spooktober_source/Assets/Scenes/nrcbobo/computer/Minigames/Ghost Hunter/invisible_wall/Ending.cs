using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour {
	public bool bad_ending;
	public Animator door;
	
	void OnTriggerEnter(Collider col) {
		if (col.tag == "Player") {
			var p = col.GetComponent<PlayerCombatSystem>();
				StaticDataLoader.event_minigame3_finished = true;
				StaticDataLoader.ending = p.has_secret_key;
				Destroy(gameObject);
		}
	}
}

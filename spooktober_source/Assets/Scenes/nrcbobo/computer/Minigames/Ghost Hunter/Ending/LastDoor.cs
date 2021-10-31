using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastDoor : MonoBehaviour {
    PlayerCombatSystem _cs;
    public bool ending;
    
    public Animator door;
    void OnTriggerEnter(Collider col) {
        if (col.tag == "Player") {
            _cs = col.GetComponent<PlayerCombatSystem>();
            _cs.old_trigger = transform.gameObject;
            _cs.on_trigger = true;
            _cs.door = door;
            Combat c = new Combat(); 
            if (!StaticDataLoader.ending && ending) {
                _cs.setCombat(c, true);
            }
                
        }
    }
  
    void OnTriggerExit(Collider col) {
        _cs.on_trigger = false;
        _cs.setCombat(new Combat());
    }
}

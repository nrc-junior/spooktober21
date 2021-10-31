using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodEnding : MonoBehaviour
{
    PlayerCombatSystem _cs;
    public Animator door;
    void OnTriggerEnter(Collider col) {
        if (col.tag == "Player") {
            _cs = col.GetComponent<PlayerCombatSystem>();
            _cs.old_trigger = transform.gameObject;
            _cs.on_trigger = true;
            _cs.door = door;
            Combat c = new Combat(); 
            _cs.setCombat(c, StaticDataLoader.ending == false);
        }
    }
  
    void OnTriggerExit(Collider col) {
        _cs.on_trigger = false;
        _cs.setCombat(new Combat());
    }

}

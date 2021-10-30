using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBlackout : MonoBehaviour {
    Moving p = null;


    void OnTriggerEnter(Collider col) {
        if (p == null) {
            p = col.GetComponent<Moving>();
        }
        p.on_blackout_trigger = true;
    }
    
    void OnTriggerExit(Collider col) {
        p.on_blackout_trigger = false;
    }
}

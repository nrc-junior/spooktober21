using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouterResetTrigger : MonoBehaviour {
    void OnTriggerStay(Collider col) {
        if (Input.GetKeyDown(KeyCode.E)) {
                col.GetComponent<PlayerData>().pc.reseted_modem();
                print("sator on");
                Destroy(gameObject);
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerOlhoMagico : MonoBehaviour {
    public GameObject corredor;
    void OnTriggerEnter(Collider col) {
        corredor.SetActive(true);
    }
    
    void OnTriggerExit(Collider col) {
        corredor.SetActive(false);
    } 
}

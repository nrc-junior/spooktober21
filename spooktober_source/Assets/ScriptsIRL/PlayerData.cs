using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour {
    public ComputerDesktop pc;
    public GameObject[] all_lights;
    public GameObject router_object;
    
    public void TurnLights(bool status) {
        foreach (var l in all_lights) {
            l.SetActive(status);
        }
    }
}

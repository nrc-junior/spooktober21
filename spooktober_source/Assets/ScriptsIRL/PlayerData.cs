using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour {
    public ComputerDesktop pc;
    public GameObject[] all_lights;
    public GameObject router_object;
    public GameObject dead_plants;
    public GameObject alive_plants;
    
    public void TurnLights(bool status) {
        foreach (var l in all_lights) {
            l.SetActive(status);
        }
    }

    public void SwitchPlants() {
        alive_plants.SetActive(false);
        dead_plants.SetActive(true);
    }

}

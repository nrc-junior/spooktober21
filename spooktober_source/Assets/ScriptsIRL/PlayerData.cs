using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour {
    public ComputerDesktop pc;
    public GameObject[] all_lights;
    public GameObject router_object;
    public GameObject dead_plants;
    public GameObject alive_plants;   
    public GameObject dead_dog;
    public GameObject alive_dog;
    public TriggerOlhoMagico olho_magico;
    
    public void TurnLights(bool status) {
        foreach (var l in all_lights) {
            l.SetActive(status);
        }
    }

    public bool DuringEvent() {
        return olho_magico.evento_tomate;
    } 
    public void SwitchPlants() {
        alive_plants.SetActive(false);
        dead_plants.SetActive(true);
    }
    
  public void NapDog() {
        alive_dog.SetActive(false);
        dead_dog.SetActive(true);
    }

}

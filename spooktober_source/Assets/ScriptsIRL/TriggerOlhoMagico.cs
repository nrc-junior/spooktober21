using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerOlhoMagico : MonoBehaviour {
    public GameObject corredor;
    [HideInInspector] public bool evento_tomate;
    bool evento_final;
    
    Moving player;
    public GameObject tomate;
    public GameObject diabo;
    
    void OnTriggerEnter(Collider col) {
        corredor.SetActive(true);
        if (evento_tomate) {
            player = col.GetComponent<Moving>();
            player.sit = true;
            GetComponent<EventController1>().Run(tomate.GetComponent<Animator>());
        }

        if (evento_final) {
            player = col.GetComponent<Moving>();
            player.sit = true;
            GetComponent<EventController2>().Run();
        }
    }

    public void EventoFinal() {
        evento_final = true;
            diabo.SetActive(true);
            tomate.SetActive(false);
    }

    public void EventoTomate() {
        evento_tomate = true;
        tomate.SetActive(true);
    }


    public GameObject Monitor;
    public End end;
    
    public void ReleasePlayer() {
        player.sit = false;
        if (evento_tomate) {
            print("entrei");
            evento_tomate = false;
            player.sit = false;
        }
        
        else if (evento_final) {
          Monitor.SetActive(true);
          end.Terminate();
        }
    }
    
    
    void OnTriggerExit(Collider col) {
        corredor.SetActive(false);
        tomate.SetActive(false);
    } 
}

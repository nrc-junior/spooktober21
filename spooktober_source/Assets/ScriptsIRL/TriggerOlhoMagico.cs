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
    }

    public void EventoTomate() {
        evento_tomate = true;
        tomate.SetActive(true);
    }
    
    public void CryTomate() {
        tomate.GetComponent<Animator>().Play("cry");    
    }
    
    public void ReleasePlayer() {
        player.sit = false;
        if (evento_tomate) {
            print("entrei");
            evento_tomate = false;
            player.sit = false;
        }
        else if (evento_final) {
            SceneManager.LoadScene(5);
        }
    }
    
    
    void OnTriggerExit(Collider col) {
        corredor.SetActive(false);
        tomate.SetActive(false);
    } 
}

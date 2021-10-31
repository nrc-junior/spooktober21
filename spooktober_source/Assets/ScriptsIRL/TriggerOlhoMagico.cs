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
            
                
            //diabo
            if (StaticDataLoader.ending) {
                GetComponent<EventController2>().Run();
            }else {
                print("texto do tomate final");
            }

        }
    }

    public void EventoFinal() {
        evento_final = true;
        
        //diabo
        if (StaticDataLoader.ending) {
            diabo.SetActive(true);
            tomate.SetActive(false);
        }else {
            diabo.SetActive(false);
            tomate.SetActive(true);
        }
    }

    public void EventoTomate() {
        evento_tomate = true;
        tomate.SetActive(true);
    }
    
    
    public void ReleasePlayer() {
        player.sit = false;
        if (evento_tomate) {
            print("entrei");
            evento_tomate = false;
            player.sit = false;
        }
        else if (evento_final) {
            if (StaticDataLoader.ending) {
                
            }else {
                
            }
        }
    }
    
    
    void OnTriggerExit(Collider col) {
        corredor.SetActive(false);
        tomate.SetActive(false);
    } 
}

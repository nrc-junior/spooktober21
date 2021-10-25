using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerDesktop : MonoBehaviour {
    public ComputerWindowOptions window_options;
    
    public void StartGame(Minigame mg) {
        window_options.gameObject.SetActive(true);
        window_options.SetMinigame(mg, gameObject);
    } 

    public void Crash(string app) {
        Debug.Log(app + " crashou.");
    }
}

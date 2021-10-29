using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerDesktop : MonoBehaviour {
    public GameObject sator; 
    public GameObject logoff;
    public bool sator_avaiable;
    bool tollbar_open;
    
    public Slider download;
    public GameObject download_error;
    
    [HideInInspector] public bool connection;
    
    #region old
    public ComputerWindowOptions window_options;
    public Sprite interneton; 
    public Sprite internetoff;
    
    public void StartGame(Minigame mg) {
        window_options.gameObject.SetActive(true);
        window_options.SetMinigame(mg, gameObject);
    } 

    public void Crash(string app) {
        Debug.Log(app + " crashou.");
    }
    #endregion
    public void ExitComputer() {
        transform.parent.gameObject.SetActive(false);
    }

    public void noConnection() {
        if (!download_error.active) {
            download.gameObject.SetActive(true);
            StartCoroutine(errorDownload());
        }
    }

    IEnumerator errorDownload() {
        while (download.value < 5) {
            download.value++;
            yield return   new  WaitForSeconds(0.1f);
            
        }
        yield return   new  WaitForSeconds(1f);
        download.gameObject.SetActive(false);
        download.value = 0;
        download_error.gameObject.SetActive(true);
    }
   
    public void CallTollbar() {
        if (tollbar_open) {
            sator.SetActive(false);
            logoff.SetActive(false);
            tollbar_open = false;
        } else {
            if (sator_avaiable) {
                sator.SetActive(true);
            }
            logoff.SetActive(true);
            tollbar_open = true;
        }
    }
    
}

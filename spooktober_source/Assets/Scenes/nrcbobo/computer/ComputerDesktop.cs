using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComputerDesktop : MonoBehaviour {
    //player inputs
    public Moving player_move;
    
    public GameObject sator; 
    public GameObject logoff;
    public bool sator_avaiable;
    bool tollbar_open;
    
    public Slider download;
    public GameObject download_error;
    


    #region sator
    public GameObject[] when_sator_on;
    public GameObject sator_page;
    public GameObject sator_blackscreen;
    public GameObject system_notification;
    bool turn_on;
    bool opened_sator;

    public GameObject sator_warn;
        
    void OnEnable() {
        IRLdataController();
        if (player_move.sit) {
            tollbar_open = false;
            sator.SetActive(false);
            logoff.SetActive(false);
        }else {
            transform.parent.gameObject.SetActive(false);
        }
    }
    
    public void TurnSator() {
        opened_sator = true;
        turn_on = !turn_on;

        foreach (GameObject o in when_sator_on){
            o.SetActive(!turn_on);
        }
        sator_page.SetActive(turn_on);
    }

    public GameObject reminder;
    IEnumerator SatorMessage() {
        yield return new WaitForSeconds(5);
        if (turn_on) {
            reminder = system_notification;
        }
        else {
            system_notification.SetActive(true);
        }
    }

    public void SatorStartEvent() {
        StartCoroutine(Blackout());
    }

    IEnumerator Blackout() {
        yield return new WaitForSeconds(2);
        sator_blackscreen.SetActive(false);
        player_move.transform.GetComponent<PlayerData>().TurnLights(false);
        player_move.Blackout();
        ExitComputer();
    }
    #endregion
    #region connection
    [HideInInspector] public bool connection;
    public Sprite online;
    public Image status_connection;

    public void ConnectInternet() {
        status_connection.sprite = online;
        sator_avaiable = false;
        connection = true;
    }
    
    #endregion
    public void OpenReminder() {
        if (reminder != null) {
            StartCoroutine(OpenReminderRot(reminder));
            reminder = null;
        }
    }

    IEnumerator OpenReminderRot(GameObject open) {
        yield return new WaitForSeconds(2);
        open.SetActive(true);
    }
    
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
        player_move.disconect();
        transform.parent.gameObject.SetActive(false);
    }

    public void noConnection(string text) {
        if (connection) { return;}
        
        if (!download_error.active) {
            if (opened_sator) {
                StartCoroutine(SatorMessage());
            }
            download.gameObject.SetActive(true);
            StartCoroutine(errorDownload(text));
        }
    }    
    
    public void noConnection(string text, bool run, int id) {
        if (!download_error.active) {
            download.gameObject.SetActive(true);
            StartCoroutine(errorDownload(text, run, id));
        }
    }

    IEnumerator errorDownload(string text, bool connect = false, int id = -1) {
        int stop = connect || id >= 0 ? 100 : 5; 
        
        download.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = text;
        while (download.value < stop) {
            download.value++;
            yield return   new  WaitForSeconds(0.1f);
        }
        yield return   new  WaitForSeconds(1f);
        download.gameObject.SetActive(false);
        download.value = 0;
        
        if (connect && id >= 0) {
            GetComponent<SceneManagement>().Load(id);
            yield return true;
        }else {
            if (connection) {
                 sator_warn.SetActive(true);           
            }else {
                download_error.gameObject.SetActive(true);
            }
        }
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
    

    public void reseted_modem() {
        sator_avaiable = true;
    }

    #region minigames

    void IRLdataController() {
        if (StaticDataLoader.event_minigame1_finished) {
            Destroy(player_move.GetComponent<PlayerData>().router_object);
            ConnectInternet();
        }
    }
    

    #endregion
    
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour {
    public GameObject[] ui;

    public void Terminate() {
        StartCoroutine(ShowCredits());
    }


    public AudioSource src;
    public AudioClip stab;
    public GameObject[] credits;
    
    IEnumerator ShowCredits() {
        foreach (GameObject o in ui) {
            o.SetActive(false);
        }
        
        src.PlayOneShot(stab);
        
        yield return new WaitForSeconds(4);
        transform.GetChild(0).gameObject.SetActive(true);
        
        int i = 0;
        while (i++ < credits.Length-1) { 
            credits[i].SetActive(true);
            yield return new WaitForSeconds(12);
            credits[i].SetActive(false);
        }
        SceneManager.LoadScene(0);
    }
}

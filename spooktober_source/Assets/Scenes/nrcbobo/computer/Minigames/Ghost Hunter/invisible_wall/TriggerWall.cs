using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWall : MonoBehaviour {
    public int points_required;
    public AudioClip afx;
    AudioSource src;
    Animator wall;
    void Start() {
        
        src = GetComponent<AudioSource>();
        wall = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Animator>();
    }
    
    void OnTriggerEnter(Collider col) {
        if (col.tag == "Player") {
            var p = col.GetComponent<PlayerCombatSystem>();
            if (p.points < points_required) {
                wall.Play("block");  
                src.PlayOneShot(afx, GlobalMixer.volume/4);
            }else {
                Destroy(gameObject);
            }
        }
    }
}

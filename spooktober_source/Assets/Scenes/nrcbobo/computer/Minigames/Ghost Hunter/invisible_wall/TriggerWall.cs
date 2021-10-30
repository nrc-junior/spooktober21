using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWall : MonoBehaviour {
    public int points_required;
    Animator wall;
    void Start() {
        wall = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Animator>();
    }
    void OnTriggerEnter(Collider col) {
        if (col.tag == "Player") {
            var p = col.GetComponent<PlayerCombatSystem>();
            if (p.points < points_required) {
                print("bu" + p.points);
              wall.Play("block");  
            }else {
                Destroy(gameObject);
            }
        }
    }
}

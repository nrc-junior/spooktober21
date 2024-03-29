using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachPos : MonoBehaviour {
  PlayerCombatSystem _cs;
  public bool has_enemy;
  public GameObject enemy;
  public int life;
  public int armor;

  public bool epicCombat;
  public bool super_epic_combat;
  public float speed;
  
  public Animator door;
  
  void OnTriggerEnter(Collider col) {
    if (col.tag == "Player") {
      _cs = col.GetComponent<PlayerCombatSystem>();
      _cs.old_trigger = transform.gameObject;
      _cs.on_trigger = true;
      _cs.door = door;
      
      Combat c = has_enemy ? new Combat(enemy,life, armor, speed, epicCombat, super_epic_combat): new Combat(); 
      _cs.setCombat(c);
    }
  }
  
  void OnTriggerExit(Collider col) {
    _cs.on_trigger = false;
    _cs.setCombat(new Combat());
  }
  
  
}

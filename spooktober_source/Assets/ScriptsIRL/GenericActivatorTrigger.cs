using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericActivatorTrigger : MonoBehaviour {
   public GameObject activation;
   Moving p;
   
   void OnTriggerEnter(Collider col) {
      if (col.tag == "Player") {
         p = col.GetComponent<Moving>();
         p.on_trigger = true;
         p.trigger = activation;
      }
   }

   void OnTriggerExit(Collider col) {
      p.on_trigger = false;
   }
}

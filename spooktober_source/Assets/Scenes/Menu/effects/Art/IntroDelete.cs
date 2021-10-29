using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroDelete : MonoBehaviour{
   void Awake() {
      StartCoroutine(DestruirSistemaOperacional());
   }

   IEnumerator DestruirSistemaOperacional() {
      yield return new WaitForSeconds(6.03f);
      Destroy(gameObject);
   }
}

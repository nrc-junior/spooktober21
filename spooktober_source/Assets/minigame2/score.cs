using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class score : MonoBehaviour
{

    public TMP_Text texto;

    int pontos = 0; 
    private void OnCollisionEnter(UnityEngine.Collision col) {
        if (col.gameObject.name == "food(Clone)") {
            pontos++;
            texto.text = pontos + " pontos!";
        }
    }
    void Update()
    {
        
    }
}

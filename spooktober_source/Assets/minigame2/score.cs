using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class score : MonoBehaviour
{

    public TMP_Text texto;
    public Animator anime;

    int pontos = 0; 
    private void OnCollisionEnter(UnityEngine.Collision col) {
        if (col.gameObject.name == "cupcake(Clone)") {
            pontos++;
            texto.text = pontos + " pontos!";
        }
        if (col.gameObject.name == "balinha(Clone)") {
            pontos++;
            texto.text = pontos + " pontos!";
        }
        if (col.gameObject.name == "pirulito(Clone)") {
            pontos++;
            texto.text = pontos + " pontos!";
        }
        if (col.gameObject.name == "vela(Clone)") {
            pontos--;
            texto.text = pontos + " pontos!";
        }
    }
    void Update()
    {
        if(pontos == 2){
            anime.SetBool ("dead", true);
        }        
    }
}

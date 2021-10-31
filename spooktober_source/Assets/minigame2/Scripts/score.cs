using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class score : MonoBehaviour {

    movement2d move;
    public Slider slider;
    public Image bar;
    
    int pontos = 0;

    public void Start() {
        move = GetComponent<movement2d>();
    }
    
    public bool isHungry() {
        return pontos <= 100;
    }

    bool buffado;
    private void OnCollisionEnter(Collision col) {
        string tag = col.transform.tag;
        if (tag == "legume" || tag == "bala") {
            switch (tag) {
                case "bala":
                    pontos++;
                    break;
                case "legume":
                    if (!buffado) {
                        buffado = true;
                        StartCoroutine(Buff());
                    }
                    break;
            }

            slider.value = pontos;
            Destroy(col.gameObject);
        }
    }

    public void ExplodeDoggo() {
        move.Explode();
    }
    
    IEnumerator Buff() {
        move.Speed = 20;
        yield return new WaitForSeconds(10);
        move.Speed = 10;
        buffado = false;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour
{
    // area jogavel q o player pode andar
    public float x_min = -7f;
    public float x_max = 15f;
    public float altura = 18f;

    public GameObject cake; 
    public GameObject bala; 
    public GameObject piru; 
    public GameObject eca; 

   void Start(){
        StartCoroutine(ChoverCake());
        StartCoroutine(ChoverBala());
        StartCoroutine(ChoverPiru());
        StartCoroutine(ChoverEca());
    }
    float time = 2;
    float a = 1/50;
    private IEnumerator ChoverCake(){
        var  cakeSpawn = 1;
        for (int i = 0; i < cakeSpawn; i++){
        var x = Random.Range(x_min,x_max);
        var drag = Random.Range(0f, 2f);
        var rain = Instantiate(cake, new Vector3(x,altura,1), Quaternion.identity);
        rain.GetComponent<Rigidbody>().drag = drag;
   }
    yield return new WaitForSeconds(time);
    time = Mathf.Lerp(time, 0.05f, a);
    yield return ChoverCake();
    }

    float timetwo = 3;
    float b = 1/50;
    private IEnumerator ChoverBala(){
        var  balaSpawn = 1;
        for (int i = 0; i < balaSpawn; i++){
        var x = Random.Range(x_min,x_max);
        var drag = Random.Range(0f, 2f);
        var rain = Instantiate(bala, new Vector3(x,altura,1), Quaternion.identity);
        rain.GetComponent<Rigidbody>().drag = drag;
   }
    yield return new WaitForSeconds(timetwo);
    time = Mathf.Lerp(timetwo, 0.05f, b);
    yield return ChoverBala();
    }

    float timethree = 4;
    float c = 1/50;
    private IEnumerator ChoverPiru(){
        var  piruSpawn = 1;
        for (int i = 0; i < piruSpawn; i++){
        var x = Random.Range(x_min,x_max);
        var drag = Random.Range(0f, 2f);
        var rain = Instantiate(piru, new Vector3(x,altura,1), Quaternion.identity);
        rain.GetComponent<Rigidbody>().drag = drag;
   }
    yield return new WaitForSeconds(timethree);
    timethree = Mathf.Lerp(timethree, 0.05f, c);
    yield return ChoverPiru();
    }

    float timefour = 5;
    float d = 1/50;
    private IEnumerator ChoverEca(){
        var  ecaSpawn = 1;
        for (int i = 0; i < ecaSpawn; i++){
        var x = Random.Range(x_min,x_max);
        var drag = Random.Range(0f, 2f);
        var rain = Instantiate(eca, new Vector3(x,altura,1), Quaternion.identity);
        rain.GetComponent<Rigidbody>().drag = drag;
   }
    yield return new WaitForSeconds(timefour);
    timefour = Mathf.Lerp(timefour, 0.05f, d);
    yield return ChoverEca();
    }
}

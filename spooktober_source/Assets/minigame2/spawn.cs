using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour
{
    // area jogavel q o player pode andar
    public float x_min = -7f;
    public float x_max = 15f;
    public float altura = 18f;

    public GameObject doces; // o lugar onde vc vai colocar os Sprites dos docinhos, com rigidy body etc...

   void Start(){
        StartCoroutine(ChoverDoce());
    }
   private IEnumerator ChoverDoce(){
       var  docesSpawn = 1;
       for (int i = 0; i < docesSpawn; i++){
           var x = Random.Range(x_min,x_max);
           var drag = Random.Range(0f, 2f);

           var rain = Instantiate(doces, new Vector3(x,altura,1), Quaternion.identity);
           rain.GetComponent<Rigidbody>().drag = drag;
       }
       var timeToWait = 2;
       yield return new WaitForSeconds(timeToWait);
       yield return ChoverDoce();
    }
}

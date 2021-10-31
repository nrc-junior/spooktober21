using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ppFoop : MonoBehaviour {
    
    public Rigidbody rb;
    float speed;
    void Start() {
        
        rb.drag = Random.Range(0, 0.1f);
        speed = Random.Range(4, 20f);
        Destroy(gameObject,Random.Range(10,20));
    }

   void Update(){
       Vector3 dir = new Vector3(0,transform.position.y,0);
       rb.AddForce(dir*-speed * Time.deltaTime);
   }
    
    private void OnCollisionEnter(UnityEngine.Collision col){
        if(col.gameObject.name == "dog"){
            Destroy(this.gameObject);
        }

    }

   
}

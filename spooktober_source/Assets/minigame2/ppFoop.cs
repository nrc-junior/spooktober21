using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ppFoop : MonoBehaviour
{
    public float Speed;
    public Rigidbody rb;

    void Update(){
        Vector3 dir = new Vector3(0,transform.position.y,0);
        rb.AddForce(dir*-Speed);
    }
    private void OnCollisionEnter(UnityEngine.Collision col){
        if(col.gameObject.name == "ground"){
            Destroy(this.gameObject);
        }
        if(col.gameObject.name == "dog"){
            Destroy(this.gameObject);
        }

    }

   
}

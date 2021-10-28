using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement2d : MonoBehaviour
{
    public float Speed = 10f;
    public Rigidbody rb;
    Vector3 mov;
    private SphereCollider col;
    
    //usar o rigidbody 3d para o vector3

    /**
    	cima: y+ x-
    	baixo y- x+
    	esquerda: x- y- 
    	direita: x + y + 
     */
    void Update(){
       
	    Vector3 dir = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
	    
	    mov = new Vector3(dir.x,0,0);

        if (col == true){
            Destroy(this.gameObject);
        }
    }

    void FixedUpdate(){
        rb.MovePosition(rb.position + mov * Speed * Time.fixedDeltaTime);
    }
}

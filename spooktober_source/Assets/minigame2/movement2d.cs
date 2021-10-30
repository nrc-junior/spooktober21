using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement2d : MonoBehaviour
{
    public float Speed ;
    public Rigidbody rb;
    Vector3 mov;
    public Animator anime;
    
    //usar o rigidbody 3d para o vector3

    /**
    	cima: y+ x-
    	baixo y- x+
    	esquerda: x- y- 
    	direita: x + y + 
     */
    void Update(){
       
	    Vector3 dir = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
	    
        if(transform.position.x + dir.x > 13.7 || transform.position.x + dir.x < -4.45  ){
            dir.x = 0;
        }
        mov = new Vector3(dir.x,0,0);



        anime.SetFloat ("speed", Mathf.Abs(dir.x));

    }

    void FixedUpdate(){
        rb.MovePosition(rb.position + mov * Speed * Time.fixedDeltaTime);
    }
}

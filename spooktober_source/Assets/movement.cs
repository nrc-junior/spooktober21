using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float Speed = 10f;
    public Rigidbody rb;
    Vector3 mov;
    
    //usar o rigidbody 3d para o vector3

    /**
    	cima: y+ x-
    	baixo y- x+
    	esquerda: x- y- 
    	direita: x + y + 
     */
    void Update(){
       
	    Vector2 dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
	    if (dir != Vector2.zero) {
		    if(!(new Vector2(Mathf.Abs(dir.x), Mathf.Abs(dir.y)) == Vector2.one)){
				dir = 
			    dir.x > 0 && dir.y == 0 ? new Vector2( dir.x,dir.x) : // vai pra direita? 
			    dir.x < 0 && dir.y == 0 ? new Vector2(dir.x,dir.x) : // vai pra esquerda? se n é nenhum, então 
				new Vector2(-dir.y, dir.y); // cima & baixo
		    }else {
			    print(dir);
			    dir = 
				    dir.x > 0 && dir.y < 0 ? new Vector2( dir.x*1.5f,-dir.y/4) : // vai pra direita pra baixo?
				    dir.x > 0 && dir.y > 0 ? new Vector2(-dir.x/4, dir.y*1.5f) : // vai pra esquerda?  
				    dir.x < 0 && dir.y < 0 ? new Vector2(-dir.x/4, dir.y*1.5f) : // vai pra esquerda pra baixo?  
				    new Vector2(dir.x*1.5f, -dir.y/5); // cima & baixo
		    }
		    
	    }
	    
	    mov = new Vector3(dir.x,0,dir.y);
    }

    void FixedUpdate(){
        rb.MovePosition(rb.position + mov * Speed * Time.fixedDeltaTime);
    }
}

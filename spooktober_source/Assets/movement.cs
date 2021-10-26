using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float Speed = 10f;
    public Rigidbody rb;
    Vector3 mov;
    
    //usar o rigidbody 3d para o vector3

    void Update(){
        mov.x = Input.GetAxisRaw("Horizontal");
        mov.y = 0f;
        mov.z = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate(){
        rb.MovePosition(rb.position + mov * Speed * Time.fixedDeltaTime);
    }
}

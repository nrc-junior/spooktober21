using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moviment : MonoBehaviour
{
    public float Speed = 10f;
    public Rigidbody rb;
    Vector3 movement;
    
    //usar o rigidbody 3d para o vector3

    void Update(){
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = 0f;
        movement.z = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate(){
        rb.MovePosition(rb.position + movement * Speed * Time.fixedDeltaTime);
    }
}

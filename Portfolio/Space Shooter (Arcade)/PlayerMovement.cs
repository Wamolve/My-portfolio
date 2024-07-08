using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 10;
    public AudioSource soundExplosion;
    void Start(){
        rb = GetComponent<Rigidbody>();
    }
    void Update(){
        float ver = Input.GetAxis("Vertical");
        float hor = Input.GetAxis ("Horizontal");
        Vector3 move = new Vector3(hor, ver, 0);
        move .Normalize();
        rb.AddRelativeForce(move * speed);
        rb.AddRelativeForce(-rb.velocity);
    }
    private void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "Meteorit"){
            soundExplosion.Play();
        }
    }
}

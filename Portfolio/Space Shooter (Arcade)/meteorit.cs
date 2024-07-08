using System.Collections;
using System. Collections.Generic;
using UnityEngine;
public class meteorit : MonoBehaviour{
    private Rigidbody rb;
    public float speed = 0.0001f;
    public float angularSpeed = 2;
    public float minSize = 0.7f;
    public float maxSize = 10;
    private float size;
    public GameObject explosion;
    public PlayerHP playerHP;
    void Start()
    {
        playerHP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHP>();
        size = Random.Range(minSize, maxSize);
        transform.localScale = transform.localScale * size;
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.back * speed/size;
        rb.angularVelocity = Random.onUnitSphere * angularSpeed;
        Destroy(gameObject, 8);
    }
    private void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Player")
        {
            GameObject explos = Instantiate(explosion, transform.position, transform.rotation);
            explos.transform.localScale = explos.transform.localScale * size;
            playerHP.damage((int)size);
            Destroy(gameObject);
        }
    }
    public void Explosion(){
        GameObject explos = Instantiate(explosion, transform.position, transform.rotation);
        explos.transform.localScale = explos.transform.localScale * size;
        Destroy(gameObject);
    }
    void Update(){
        transform.position += new Vector3(0, 0, speed);
    }
}
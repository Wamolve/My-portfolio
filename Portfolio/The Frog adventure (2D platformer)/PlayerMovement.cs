using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5;
    public float jump = 3;
    public int lives = 5;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private bool checkEarth;
    private Animator animator;
    public Text textLives;
    public bool isRight = true;
    void Start(){
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    private States State{
        get{
            return (States)animator.GetInteger("state");
        }
        set
        {
            animator.SetInteger("State", (int)value);
        }
    }
    void Walk(){
        if(checkEarth == true){
            State = States.walk;
        }
        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards (transform.position, transform.position + dir, speed * Time.deltaTime);
        sprite.flipX = dir.x < 0;
        if(dir.x > 0){
            isRight = true;
        }
        else{
            isRight = false;
        }
    }
    void Jump(){
        rb.AddForce(transform.up * jump, ForceMode2D.Impulse);
    }
    public void Damage(){
        lives--;
    }
    void CheckEarth()
    {
        if(checkEarth == false){
            State = States.jump;
        }
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 1f);
        checkEarth = collider.Length > 1;
    }
    void Update()
    {
        textLives.text = "Health - " + lives;
        CheckEarth();
        if(Input.GetMouseButtonDown(0)){
            State = States.attack;
        }
        else if(checkEarth == true){
            State = States.idle;
        }
        if (Input.GetButton("Horizontal")){
            Walk();
        }
        if (Input.GetButtonDown ("Jump") && checkEarth == true){
            Jump();
        }
        if(lives <= 0){
            Destroy(gameObject);
        }
    }
}
public enum States{
    idle,
    walk,
    jump,
    attack
}

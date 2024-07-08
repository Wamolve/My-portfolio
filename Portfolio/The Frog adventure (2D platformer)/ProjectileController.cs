using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawnPoint;
    PlayerMovement playerposition;
    public GameObject Player;
    void Start(){
        playerposition = Player.GetComponent<PlayerMovement>();
    }
    void Update(){
        if (Input.GetMouseButtonDown(0))
        {
            float angle = playerposition.isRight ? 0 : 180;
            Instantiate(bullet, spawnPoint.position, Quaternion.Euler(new Vector3(0, 0, angle)));
        }
    }
}

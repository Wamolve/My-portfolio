using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    public AudioSource AudioSource;
    public AudioClip impact;

    public void Start(){
        AudioSource = this.GetComponent<AudioSource>();
    }

    public void OnTriggerEnter(Collider col){
        if(col.gameObject.tag == "Player"){
            AudioSource.PlayOneShot(impact, 0.1f);
        }
    }
}

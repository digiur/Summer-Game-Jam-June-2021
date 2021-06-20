using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindController : MonoBehaviour
{
    private AudioSource _as;
    public AudioClip audioClip;


    void Awake(){
       _as = GetComponent<AudioSource>();
    }

    void Start(){
        _as.PlayOneShot(_as.clip);
    }
}
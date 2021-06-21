using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCrackController : MonoBehaviour
{

    private GameObject player;
    void Start(){
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter(Collider other)
    {
        print("in contact with " + other.transform.name);
    }

    void OnTriggerExit(Collider other)
    {
        print("No longer in contact with " + other.transform.name);
    }




}
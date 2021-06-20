using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float backSpeed;
    [SerializeField]
    private float turnSpeed;

    public bool windLeft = false;
    public bool windRight = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("forward"))
        {
            Vector3 pos = transform.position;
            pos.z += walkSpeed * Time.deltaTime;
            transform.position = pos;
        }
        if (Input.GetButton("back"))
        {
            Vector3 pos = transform.position;
            pos.z -= backSpeed * Time.deltaTime;
            transform.position = pos;
        }
        if (Input.GetButton("right"))
        {
            Vector3 pos = transform.position;
            pos.x += turnSpeed * Time.deltaTime;
            transform.position = pos;
        }
        if (Input.GetButton("left"))
        {
            Vector3 pos = transform.position;
            pos.x -= turnSpeed * Time.deltaTime;
            transform.position = pos;
        }

        if(windLeft){
            Vector3 pos = transform.position;
            pos.x -= turnSpeed/2 * Time.deltaTime;
            transform.position = pos;
            if(pos.x >= 5 || pos.x <= -5){
                Debug.Log("YOU ARE LOST");
            }
        }
        if(windRight){
            Vector3 pos = transform.position;
            pos.x += turnSpeed/2 * Time.deltaTime;
            transform.position = pos;
            if(pos.x >= 5 || pos.x <= -5){
                Debug.Log("YOU ARE LOST");
            }
        }


    }
}

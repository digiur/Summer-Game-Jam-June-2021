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
            pos.z += walkSpeed / 100;
            transform.position = pos;
        }
        if (Input.GetButton("back"))
        {
            Vector3 pos = transform.position;
            pos.z -= backSpeed / 100;
            transform.position = pos;
        }
        if (Input.GetButton("right"))
        {
            Vector3 pos = transform.position;
            pos.x += turnSpeed / 100;
            transform.position = pos;
        }
        if (Input.GetButton("left"))
        {
            Vector3 pos = transform.position;
            pos.x -= turnSpeed / 100;
            transform.position = pos;
        }
    }
}

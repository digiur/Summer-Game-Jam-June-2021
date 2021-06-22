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
    [SerializeField]
    private float windMultiplier;
    [SerializeField]
    private float lostDistance;

    private bool transitioned = false;

    public bool windLeft = false;
    public bool windRight = false;
    public bool iceCracked = false;
    private TransitionController tc;
    [SerializeField]
    Animator torso;
    [SerializeField]
    Animator arm;
    [SerializeField]
    Animator legs;
    // Start is called before the first frame update
    void Start()
    {
        tc = GetComponent<TransitionController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (tc.transitioned) return;
        bool walking = false;
        if (Input.GetButton("forward") && !iceCracked)
        {
            Vector3 pos = transform.position;
            pos.z += walkSpeed * Time.deltaTime;
            transform.position = pos;
            walking = true;
        }
        if (Input.GetButton("back"))
        {
            Vector3 pos = transform.position;
            pos.z -= backSpeed * Time.deltaTime;
            transform.position = pos;
            walking = true;
        }
        if (Input.GetButton("right"))
        {
            Vector3 pos = transform.position;
            pos.x += turnSpeed * Time.deltaTime;
            transform.position = pos;
            walking = true;
        }
        if (Input.GetButton("left"))
        {
            Vector3 pos = transform.position;
            pos.x -= turnSpeed * Time.deltaTime;
            transform.position = pos;
            walking = true;
        }
        torso.SetBool("Walking", walking);
        legs.SetBool("Walking", walking);
        arm.SetBool("Walking", walking);
        arm.SetBool("Torch", Input.GetKey(KeyCode.E));


        if (windLeft)
        {
            Vector3 pos = transform.position;
            pos.x -= turnSpeed * windMultiplier * Time.deltaTime;
            transform.position = pos;
        }
        if (windRight)
        {
            Vector3 pos = transform.position;
            pos.x += turnSpeed * windMultiplier * Time.deltaTime;
            transform.position = pos;
        }
        if (transform.position.x >= lostDistance || transform.position.x <= -lostDistance)
        {
            if (!transitioned)
            {
                transitioned = true;
                tc.Transition("WentOffCourse");
            }
        }
    }
}

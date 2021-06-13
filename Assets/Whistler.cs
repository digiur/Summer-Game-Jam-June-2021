using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whistler : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float returnSpeedX;
    [SerializeField]
    private float returnSpeedY;
    [SerializeField]
    private float turnSpeed;
    [SerializeField]
    private float yClampMin;
    [SerializeField]
    private float yClampMax;
    [SerializeField]
    private float xClampMin;
    [SerializeField]
    private float xClampMax;
    [SerializeField]
    private float homeSize;
    private GameObject player;
    Vector3 homePosition;

    // Start is called before the first frame update
    void Start()
    {
        homePosition = transform.localPosition;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.localPosition;
        bool xIdle = true;
        bool yIdle = true;

        if (Input.GetButton("forward"))
        {
            pos.y -= walkSpeed * Time.deltaTime;
            yIdle = false;
        }
        if (Input.GetButton("back"))
        {
            pos.y += walkSpeed * Time.deltaTime;
            yIdle = false;
        }
        if (Input.GetButton("right"))
        {
            pos.x -= turnSpeed * Time.deltaTime;
            xIdle = false;
        }
        if (Input.GetButton("left"))
        {
            pos.x += turnSpeed * Time.deltaTime;
            xIdle = false;
        }

        pos.y = Mathf.Clamp(pos.y, yClampMin, yClampMax);
        pos.x = Mathf.Clamp(pos.x, xClampMin, xClampMax);

        if (xIdle && Mathf.Abs(pos.x - homePosition.x) > homeSize)
            pos.x += (pos.x < homePosition.x ? 1 : -1) * returnSpeedX * Time.deltaTime;

        if (yIdle && Mathf.Abs(pos.y - homePosition.y) > homeSize)
            pos.y += (pos.y < homePosition.y ? 1 : -1) * returnSpeedY * Time.deltaTime;

        if (pos.y < player.transform.localPosition.y)
            pos.z = 1.9f;
        else
            pos.z = 2.1f;

        transform.localPosition = pos;
    }
}

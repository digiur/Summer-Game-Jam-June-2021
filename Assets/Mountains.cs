using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mountains : MonoBehaviour
{
    // Start is called before the first frame update
    private float myX;
    void Start()
    {
        myX = transform.position.x;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(myX, transform.position.y, transform.position.z);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerspectiveController : MonoBehaviour
{
    [SerializeField]
    private float farDistance;

    [SerializeField]
    private float nearDistance;
    [SerializeField]
    private float epsilon;

    private GameObject[] gameObjects;
    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Grounded");
        Vector3 myPos = transform.position;
        float worldFar = myPos.z + farDistance;
        float worldNear = myPos.z - nearDistance;
        foreach (GameObject obj in gameObjects)
        {

            Vector3 objPos = obj.transform.position;
            if (objPos.z < worldFar && objPos.z > myPos.z + epsilon)
            {
                //far
                Vector3 newPosition = obj.transform.position;
                float t = Mathf.InverseLerp(worldFar, myPos.z + epsilon, newPosition.z);
                newPosition.y = t * myPos.y;
                Debug.Log("start" + myPos.z + epsilon);
                Debug.Log("end" + worldFar);
                Debug.Log("t" + newPosition.z);
                Debug.Log("result" + t);
                obj.transform.position = newPosition;
            }
            if (objPos.z < myPos.z - epsilon && objPos.z > worldNear)
            {
                //near
                Vector3 newPosition = obj.transform.position;
                float t = Mathf.InverseLerp(worldNear, myPos.z - epsilon, newPosition.z);
                newPosition.y = t * myPos.y;
                obj.transform.position = newPosition;
            }
        }
    }
}

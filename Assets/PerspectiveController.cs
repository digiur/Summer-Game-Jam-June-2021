using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerspectiveController : MonoBehaviour
{
    [SerializeField]
    private float minScale;
    [SerializeField]
    private float maxScale;
    [SerializeField]
    private float farDistance;
    [SerializeField]
    private float nearDistance;
    [SerializeField]
    private float epsilon;

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

            //scale
            Vector3 newScale = obj.transform.localScale;
            float t = Mathf.InverseLerp(myPos.z + epsilon, worldNear, objPos.z);
            newScale.x = newScale.y = Mathf.Lerp(minScale, maxScale, t);
            obj.transform.localScale = newScale;

            if (objPos.z < worldFar && objPos.z > myPos.z + epsilon)
            {
                //far y
                Vector3 newPosition = objPos;
                t = Mathf.InverseLerp(worldFar, myPos.z + epsilon, newPosition.z);
                newPosition.y = t * myPos.y;
                obj.transform.position = newPosition;
            }
            if (objPos.z < myPos.z - epsilon && objPos.z > worldNear)
            {
                //near y
                Vector3 newPosition = objPos;
                t = Mathf.InverseLerp(worldNear, myPos.z - epsilon, newPosition.z);
                newPosition.y = t * myPos.y;
                obj.transform.position = newPosition;
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    [SerializeField]
    private float timeBetweenSteps;
    [SerializeField]
    private GameObject footstepPrefab;
    [SerializeField]
    private Transform rightTransform;
    [SerializeField]
    private Transform leftTransform;
    [SerializeField]
    private float forwardOffset;
    private float time;
    private Boolean right;

    // Start is called before the first frame update
    void Start()
    {
        right = true;
        time = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if ((time += Time.deltaTime) > timeBetweenSteps)
        {
            time = 0f;
            if (right)
            {
                Vector3 pos = rightTransform.position;
                pos.z += forwardOffset;
                Instantiate(footstepPrefab, pos, Quaternion.identity);
            }
            else
            {
                Vector3 pos = leftTransform.position;
                pos.z += forwardOffset;
                Instantiate(footstepPrefab, pos, Quaternion.identity);
            }
            right = !right;
        }
    }
}

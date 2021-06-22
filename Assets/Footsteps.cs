using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    [SerializeField]
    private GameObject footstepPrefab;
    [SerializeField]
    private Transform rightTransform;
    [SerializeField]
    private Transform leftTransform;
    [SerializeField]
    private float forwardOffset;
    private Boolean right = true;

    private AudioSource _as;
    public AudioClip[] audioClipArray;

    void Awake()
    {
        _as = GetComponent<AudioSource>();
    }

    public void makeFootstep()
    {
        _as.clip = audioClipArray[UnityEngine.Random.Range(0, audioClipArray.Length)];
        _as.PlayOneShot(_as.clip, 0.33f);
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

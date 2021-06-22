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
    [SerializeField]
    private float timeBetweenFootsteps;
    [SerializeField]
    private bool auto;
    private float time;
    private Boolean right = true;

    private AudioSource _as;
    public AudioClip[] audioClipArray;

    void Awake()
    {
        _as = GetComponent<AudioSource>();
        time = timeBetweenFootsteps;
    }

    void Update()
    {
        if (!auto) return;

        if ((time -= Time.deltaTime) < 0)
        {
            time = timeBetweenFootsteps;
            makeFootstep();
        }
    }

    public void makeFootstep()
    {
        if (right)
        {
            Vector3 pos = rightTransform.position;
            pos.z += forwardOffset;
            Instantiate(footstepPrefab, pos, Quaternion.identity);
            _as.clip = audioClipArray[UnityEngine.Random.Range(0, audioClipArray.Length)];
            _as.PlayOneShot(_as.clip);
        }
        else
        {
            Vector3 pos = leftTransform.position;
            pos.z += forwardOffset;
            Instantiate(footstepPrefab, pos, Quaternion.identity);
            _as.clip = audioClipArray[UnityEngine.Random.Range(0, audioClipArray.Length)];
            _as.PlayOneShot(_as.clip);
        }
        right = !right;
    }
}

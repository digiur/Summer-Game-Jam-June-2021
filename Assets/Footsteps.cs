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

    private AudioSource _as;
    public AudioClip[] audioClipArray;

    void Awake(){
       _as = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
       
        right = true;
        time = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void makeFootstep(){
            if (right)
            {
                Vector3 pos = rightTransform.position;
                pos.z += forwardOffset;
                Instantiate(footstepPrefab, pos, Quaternion.identity);
                 _as.clip= audioClipArray[UnityEngine.Random.Range(0, audioClipArray.Length)];
                _as.PlayOneShot(_as.clip);
            }
            else
            {
                Vector3 pos = leftTransform.position;
                pos.z += forwardOffset;
                Instantiate(footstepPrefab, pos, Quaternion.identity);
                 _as.clip= audioClipArray[UnityEngine.Random.Range(0, audioClipArray.Length)];
                _as.PlayOneShot(_as.clip);
            }
            right = !right;
        }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem top1;
    [SerializeField]
    private ParticleSystem top2;
    [SerializeField]
    private ParticleSystem top3;
    [SerializeField]
    private ParticleSystem left1;
    [SerializeField]
    private ParticleSystem left2;
    [SerializeField]
    private ParticleSystem left3;
    [SerializeField]
    private ParticleSystem right1;
    [SerializeField]
    private ParticleSystem right2;
    [SerializeField]
    private ParticleSystem right3;
    [SerializeField]
    private ParticleSystemForceField forceField;
    [SerializeField]
    private float windSpeed;

    // Update is called once per frame
    void Update()
    {
        ParticleSystem.EmissionModule emission;
        if (Input.GetButton("forward"))
        {
            emission = top1.emission;
            emission.enabled = true;
            emission = top2.emission;
            emission.enabled = true;
            emission = top3.emission;
            emission.enabled = true;

            emission = right1.emission;
            emission.enabled = false;
            emission = right2.emission;
            emission.enabled = false;
            emission = right3.emission;
            emission.enabled = false;

            emission = left1.emission;
            emission.enabled = false;
            emission = left2.emission;
            emission.enabled = false;
            emission = left3.emission;
            emission.enabled = false;

            forceField.directionX = 0f;
        }

        if (Input.GetButton("right"))
        {
            emission = top1.emission;
            emission.enabled = false;
            emission = top2.emission;
            emission.enabled = false;
            emission = top3.emission;
            emission.enabled = false;

            emission = right1.emission;
            emission.enabled = true;
            emission = right2.emission;
            emission.enabled = true;
            emission = right3.emission;
            emission.enabled = true;

            emission = left1.emission;
            emission.enabled = false;
            emission = left2.emission;
            emission.enabled = false;
            emission = left3.emission;
            emission.enabled = false;

            forceField.directionX = -windSpeed;
        }

        if (Input.GetButton("left"))
        {
            emission = top1.emission;
            emission.enabled = false;
            emission = top2.emission;
            emission.enabled = false;
            emission = top3.emission;
            emission.enabled = false;

            emission = right1.emission;
            emission.enabled = false;
            emission = right2.emission;
            emission.enabled = false;
            emission = right3.emission;
            emission.enabled = false;

            emission = left1.emission;
            emission.enabled = true;
            emission = left2.emission;
            emission.enabled = true;
            emission = left3.emission;
            emission.enabled = true;

            forceField.directionX = windSpeed;
        }
        if (Input.GetButton("back"))
        {
            emission = top1.emission;
            emission.enabled = false;
            emission = top2.emission;
            emission.enabled = false;
            emission = top3.emission;
            emission.enabled = false;

            emission = right1.emission;
            emission.enabled = false;
            emission = right2.emission;
            emission.enabled = false;
            emission = right3.emission;
            emission.enabled = false;

            emission = left1.emission;
            emission.enabled = false;
            emission = left2.emission;
            emission.enabled = false;
            emission = left3.emission;
            emission.enabled = false;

            forceField.directionX = 0;
        }
    }
}

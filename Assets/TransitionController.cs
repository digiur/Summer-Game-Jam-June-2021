using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionController : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem white;
    [SerializeField]
    private ParticleSystem black;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Debug Reset"))
        {
            StartCoroutine(Transition());
        }
    }

    IEnumerator Transition()
    {
        white.Play();
        yield return new WaitForSeconds(2);
        black.Play();
        yield return new WaitForSeconds(4);
        UnityEngine.SceneManagement.SceneManager.LoadScene("StageTwo");
    }
}

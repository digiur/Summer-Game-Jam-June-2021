using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionController : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem white;
    [SerializeField]
    private ParticleSystem black;
    [SerializeField]
    private bool autoTransitionToMainMenu = false;
    [SerializeField]
    private float autoWaitTime = 0f;

    public bool transitioned = false;

    // Update is called once per frame
    void Update()
    {
        if (autoTransitionToMainMenu && (autoWaitTime -= Time.deltaTime) <= 0 && !transitioned)
        {
            transitioned = true;
            StartCoroutine(TransitionRoutine("MainMenu"));
        }
    }

    public void Transition(string scene)
    {
        transitioned = true;
        StartCoroutine(TransitionRoutine(scene));
    }

    IEnumerator TransitionRoutine(string scene)
    {
        white.Play();
        yield return new WaitForSeconds(2);
        black.Play();
        yield return new WaitForSeconds(3.5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
    }
}

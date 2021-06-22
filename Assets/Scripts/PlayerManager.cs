using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] public GameObject ghost;
    [SerializeField] public GameObject wind;
    [SerializeField] public GameObject crack;

    [SerializeField]
    private Transform rightTransform;
    [SerializeField]
    private Transform leftTransform;

    private List<GameObject> ghostList = new List<GameObject>();

    bool walking = false;
    bool checkingProb = false;
    bool creatingFootsteps = false;
    bool windActive = false;
    bool win = false;
    bool transitioned = false;
    bool dying = false;

    private GameObject player;
    private Footsteps FootstepsScript;
    private GameObject rig;
    private MovementController mc;
    private ParticleController pc;
    private TransitionController tc;

    int result = -1;

    int windProb;
    int ghostProb;
    int iceProb;

    [SerializeField]
    int windProbability;
    [SerializeField]
    int ghostProbability;
    [SerializeField]
    int iceProbability;
    [SerializeField]
    float winDistance = 10f;
    [SerializeField]
    float spawnSpread;
    [SerializeField]
    float spawnDistance;
    [SerializeField]
    float windDuration;
    [SerializeField]
    float secondsBetweenSpawnAttempts;
    [SerializeField]
    float secondsBetweenFootsteps;
    [SerializeField]
    float iceDuration;
    [SerializeField]
    float iceRange;
    [SerializeField]
    string winStage;
    int lives = 3;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        FootstepsScript = player.GetComponent<Footsteps>();
        rig = GameObject.FindGameObjectWithTag("Rig");
        mc = rig.GetComponent<MovementController>();
        pc = rig.GetComponent<ParticleController>();
        tc = rig.GetComponent<TransitionController>();

        pc.blowTopDown();
        ghostProb = ghostProbability;
        windProb = windProbability;
        iceProb = iceProbability;
    }

    void Update()
    {
        if (player.transform.position.z > winDistance)
        {
            win = true;
        }
        //Start Spawning Events
        if(!dying && !win){
            if (!checkingProb)
            {
                StartCoroutine(checkProbability());
            }
            Walking();
            //Raise Lamp
            raiseLamp();
        }




        if (win && !transitioned)
        {
            transitioned = true;
            tc.Transition(winStage);
        }

    }

    //SPAWNING METHODS
    private void SpawnWind()
    {
        StartCoroutine(windHowling(windDuration));
    }

    private void SpawnGhost()
    {
        // Debug.Log("SPAWNED GHOST");
        Instantiate(ghost, new Vector3((player.transform.position.x + Random.Range(-spawnSpread, spawnSpread)),
                                                (player.transform.position.y),
                                                (player.transform.position.z + spawnDistance)), Quaternion.identity);
    }

    private void SpawnIce()
    {
        int rightOrLeft = Random.Range(0, 100);
        mc.iceCracked = true;
        if (rightOrLeft <= 50)
        {
            Vector3 pos = rightTransform.position;
            pos.z += 1;
            StartCoroutine(crackIce(Instantiate(crack, pos, Quaternion.identity)));
        }
        else
        {
            Vector3 pos = leftTransform.position;
            pos.z += 1;
            StartCoroutine(crackIce(Instantiate(crack, pos, Quaternion.identity)));
        }

    }

    //COLLISION
    void OnTriggerEnter(Collider collision)
    {
        //Debug.Log("COLLIDED");
        if (collision.gameObject.tag == "Ghost")
        {
            ghostList.Add(collision.gameObject);
        }
        if (collision.gameObject.tag == "crack")
        {

        }
    }

    //REACTION METHODS
    private void raiseLamp()
    {
        if (Input.GetKey(KeyCode.E))
        {
            List<GameObject> toRemove = new List<GameObject>();
            foreach (GameObject obj in ghostList)
            {
                obj.GetComponent<Ghost>().Die();
                toRemove.Add(obj);
            }
            foreach (GameObject obj in toRemove)
            {
                ghostList.Remove(obj);
            }
        }
    }

    //MOVEMENT
    void Walking()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            walking = true;
            if (!creatingFootsteps)
            {
                StartCoroutine(createFootsteps());
            }
        }
        walking = false;
    }

    //IEnumerators

    public IEnumerator checkProbability()
    {

        checkingProb = true;
        bool runLoop = true;
        yield return new WaitForSeconds(4);

        while (runLoop)
        {
            result = GameManager.EventProbabilityCounter(windProb, ghostProb, iceProb);
            //Debug.Log(result);
            if (result != -1)
            {
                if (result == 0 && !windActive)
                {
                    //spawn wind
                    windProb = windProbability;
                    SpawnWind();
                    //Debug.Log("SPAWNED WIND");
                }
                else if (result == 1)
                {
                    //spawn ghost
                    ghostProb = ghostProbability;
                    SpawnGhost();
                }
                else if (result == 2)
                {
                    iceProb = iceProbability;
                    SpawnIce();
                    //Debug.Log("SPAWNED ICE");
                }
                checkingProb = false;
                runLoop = false;
                yield return result;
            }
            else
            {
                windProb--;
                ghostProb--;
                iceProb--;
                yield return new WaitForSeconds(secondsBetweenSpawnAttempts);
            }
        }
    }

    public IEnumerator createFootsteps()
    {
        while (walking)
        {
            creatingFootsteps = true;
            FootstepsScript.makeFootstep();
            yield return new WaitForSeconds(secondsBetweenFootsteps);
        }
        creatingFootsteps = false;
    }

    public IEnumerator windHowling(float duration)
    {
        windActive = true;
        int direction = Random.Range(0, 100);
        GameObject windOBJ = Instantiate(wind, new Vector3(0, 0, 0), Quaternion.identity);
        if (direction <= 50)
        {
            mc.windLeft = true;
            pc.blowRightToLeft();
        }
        else
        {
            mc.windRight = true;
            pc.blowLeftToRight();
        }
        yield return new WaitForSeconds(duration);
        mc.windLeft = false;
        mc.windRight = false;
        Destroy(windOBJ);
        pc.blowTopDown();
        windActive = false;
    }

    public IEnumerator crackIce(GameObject iceCrack)
    {
        yield return new WaitForSeconds(iceDuration);
        if (((player.transform.position.x - iceCrack.transform.position.x) <= iceRange) &&
            ((player.transform.position.x - iceCrack.transform.position.x) >= -iceRange))
        {
            dying = true;
            if (!transitioned)
            {
                transitioned = true;
                tc.Transition("FellInIce");
            }
        }
        else
        {
            //Debug.Log("You have bypassed the ice crack");
        }
        mc.iceCracked = false;
        yield return null;
    }

    public IEnumerator startWinCounter(float durationToWin)
    {
        yield return new WaitForSeconds(durationToWin);
        win = true;
    }

    //External use methods
    public void takeDamage()
    {
        dying = true;
        if (!transitioned)
        {
            transitioned = true;
            tc.Transition("EatenByGhosts");
        }
    }

}

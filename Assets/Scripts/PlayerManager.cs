using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] public GameObject ghost;
    [SerializeField] public GameObject wind;
    [SerializeField] public GameObject crack;

    private List<GameObject> ghostList = new List<GameObject>();

    bool walking = false;
    bool checkingProb = false;
    bool creatingFootsteps = false;
    bool windActive = false;

    private GameObject player;
    private Footsteps FootstepsScript;
    private GameObject rig;
    private MovementController mc;

    int result = -1;

    int windProb = 10;
    int ghostProb = 10;
    int iceProb = 10;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        FootstepsScript = player.GetComponent<Footsteps>();
        rig = GameObject.FindGameObjectWithTag("Rig");
        mc = rig.GetComponent<MovementController>();

    }

    void Update()
    {
        //Start Spawning Events
         if(!checkingProb){
             StartCoroutine(checkProbability());
        }

        Walking();

        //Raise Lamp
        raiseLamp();

    }

    //SPAWNING METHODS
    private void SpawnWind(){
        StartCoroutine(windHowling(5));
    }

    private void SpawnGhost(){
       // Debug.Log("SPAWNED GHOST");
        Instantiate(ghost, new Vector3((player.transform.position.x + Random.Range(-7, 7)),
                                                (player.transform.position.y), 
                                                (player.transform.position.z + 15)), Quaternion.identity);
    }

    private void SpawnIce(){

    }

    //COLLISION
    void OnTriggerEnter(Collider collision){
        Debug.Log("COLLIDED");
        if(collision.gameObject.tag == "Sprite"){
            ghostList.Add(collision.gameObject);
        }
    }

    //REACTION METHODS
    private void raiseLamp(){
        if(Input.GetKey(KeyCode.E)){
            foreach(GameObject obj in ghostList){
                ghostList.Remove(obj);
                Destroy(obj);
            }
        }
    }

    //MOVEMENT
    void Walking(){
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)){
            walking = true;
            if(!creatingFootsteps){
                StartCoroutine(createFootsteps());
            }
        }
        walking = false;
    }

    //IEnumerators

        public IEnumerator checkProbability(){
        checkingProb = true;
        bool runLoop = true;

            while(runLoop){
                result = GameManager.EventProbabilityCounter(windProb, ghostProb, iceProb);
                //Debug.Log(result);
                if(result != -1){
                    if(result == 0 && !windActive){
                        //spawn wind
                        windProb = 20;
                        SpawnWind();
                        //Debug.Log("SPAWNED WIND");
                    } else if(result == 1){
                        //spawn ghost
                        ghostProb = 20;
                        SpawnGhost();
                    } else if(result == 2){
                        iceProb =20;
                        //Debug.Log("SPAWNED ICE");
                    }
                    checkingProb = false;
                    runLoop = false;
                    yield return result;
                } else{
                    windProb--;
                    ghostProb--;
                    iceProb--;
                    yield return new WaitForSeconds(1);
                }
            }
        }

        public IEnumerator createFootsteps(){
            while(walking){
                creatingFootsteps = true;
                FootstepsScript.makeFootstep();
                yield return new WaitForSeconds(1);
            }
            creatingFootsteps = false;
        }

        public IEnumerator windHowling(int duration){
            windActive = true;
            int direction = Random.Range(0,1);
            GameObject windOBJ = Instantiate(wind, new Vector3(0,0,0), Quaternion.identity);
            if(direction == 0){
                mc.windLeft = true;
            } else {
                mc.windRight = true;
            }
            yield return new WaitForSeconds(duration);
            mc.windLeft = false;
            mc.windRight = false;
            Destroy(windOBJ);
            windActive = false;

        }

    //External use methods
    public void takeDamage(){
        Debug.Log("Take Damage");
    }

}

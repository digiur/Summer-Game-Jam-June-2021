using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] public GameObject ghost;
    [SerializeField] public GameObject wind;
    [SerializeField] public GameObject crack;

    private List<GameObject> ghostList = new List<GameObject>();

    private GameObject player;
    bool checkingProb = false;
    int result = -1;

        int windProb = 10;
        int ghostProb = 10;
        int iceProb = 10;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        //Start Spawning Events
         if(!checkingProb){
             StartCoroutine(checkProbability());
        }

        //Raise Lamp
        raiseLamp();

    }

    //SPAWNING METHODS
    private void SpawnWind(){

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

    //IEnumerators

        public IEnumerator checkProbability(){
        checkingProb = true;
        bool runLoop = true;

            while(runLoop){
                result = GameManager.EventProbabilityCounter(windProb, ghostProb, iceProb);
                //Debug.Log(result);
                if(result != -1){
                    if(result == 0){
                        //spawn wind
                        windProb = 10;
                        //Debug.Log("SPAWNED WIND");
                    } else if(result == 1){
                        //spawn ghost
                        ghostProb = 10;
                        SpawnGhost();
                    } else if(result == 2){
                        iceProb =10;
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



}

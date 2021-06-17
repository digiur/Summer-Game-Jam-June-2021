using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameManager 
{

       public static int returnedEventNumber = -1;

       static int distanceTraveled = 0;
       static int leftDeviationLimit = 45;
       static int rightDeviationLimit = 45;
       static int leftDeviationValue = 0;
       static int rightDeviationValue = 0;

       static bool walking = false;
       static bool checkingProb = false;


    public static GameState state;
    static void Awake(){

    }

    static void Start(){
        UpdateGameState(GameState.Phase1);
        Debug.Log("STARTED GM");
    }
    static void Update(){
        if(state == GameState.Phase1){
            if(!walking){
                //StartCoroutine("Walking", 5);
            }
        }


    }

    public static void UpdateGameState(GameState newState){
        state = newState;

        switch (newState){
            case GameState.Phase1:
                HandlePhase1();
                break;
            case GameState.Phase2:
                HandlePhase2();
                break;
            case GameState.Phase3:
                break;
            case GameState.Victory:
                break;
            case GameState.GameOver:
                break;
        }
    }

//Stage Handlers
    public static void HandlePhase1(){
       
        Debug.Log("STATE1!");

    }
    public static void HandlePhase2(){
        //getrig
        Debug.Log("STATE2!");
    }
    public enum GameState {
        Phase1,
        Phase2,
        Phase3,
        Victory,
        GameOver
    }

//Probability Method
    public static int EventProbabilityCounter(int windEventProbability, int ghostEventProbability, int iceEventProbability){
        int numToCheck = 0;


        int windNumber = Random.Range(0 , windEventProbability);
        int ghostNumber = Random.Range(0, ghostEventProbability);
        int iceNumber = Random.Range(0, iceEventProbability);
        // Debug.Log("WIND NUMBER: " + windNumber);
        //Debug.Log("GHOST NUMBER: " + ghostNumber);
        // Debug.Log("ICE NUMBER: " + iceNumber);

        Debug.Log(windEventProbability);
        Debug.Log(ghostEventProbability);
        Debug.Log(iceEventProbability);
        if(numToCheck == windNumber){
            //run wind event
            //Debug.Log("EVENT: WIND");
            return 0;

        } else if(numToCheck == ghostNumber){
            //run ghost event
            //Debug.Log("EVENT: GHOST");
            return 1;

        } else if(numToCheck == iceNumber){
            Debug.Log("EVENT: ICE");
            //run ice event
            return 2;

        } else {
            //do nothing
            return -1;
        }

    }

    //EventMethods
    // private static void StartWindEvent(){
    //     Debug.Log("STARTED WIND");
    //     windEventProbability = 100;
    // }
    // private static void StartGhostEvent(GameObject player, GameObject ghost){
    //     Debug.Log("STARTED GHOST");
    //     ghostEventProbability = 100;
    //     // instance.Instantiate(ghost, new Vector3((player.transform.position.x + Random.Range(-10, 10)),
    //     //                                 (player.transform.position.y), 
    //     //                                 (player.transform.position.z + 10)), Quaternion.identity);

    // }
    // private static void StartIceEvent(){
    //     Debug.Log("STARTED ICE");
    //     iceEventProbability = 100;
    // }


//Coroutines 
    private static IEnumerator IncreaseProbability(int eventProb){
        yield return new WaitForSeconds(1);
        eventProb--;
    }

    private static IEnumerator Walking(int distanceLimit){
        walking = true;

        while(true){
            if(distanceTraveled >= distanceLimit){
                Debug.Log("LIMIT: " + distanceLimit);
                 UpdateGameState(GameState.Phase2);
                break;
            }
            yield return new WaitForSeconds(1);
            Debug.Log("DISTANCE TRAVELED: " + distanceTraveled);
            distanceTraveled++;
        }
    }

    // public static IEnumerator checkProbability(){
    //     checkingProb = true;
    //     bool runLoop = true;
    //         while(runLoop){
    //             int result = EventProbabilityCounter();
    //             returnedEventNumber = result;
    //             if(result != -1){
    //                 Debug.Log("RETURNED IENMURATOR: " + returnedEventNumber);
    //                 runLoop = false;
    //                 checkingProb = false;

    //                 yield return result;
    //             } else{
    //                 yield return new WaitForSeconds(1);
    //             }
    //         }

    // }


    //Getters Setters
    public static bool getCheckingProb(){
        return checkingProb;
    }
    public static int getReturnedEvent(){
        if(returnedEventNumber != -1){
        Debug.Log("RESULT: " + returnedEventNumber);
        }
        return returnedEventNumber;
    }

    // public static void setWindProb(int newProb){
    //     windEventProbability = newProb;
    // }
    // public static void setGhostProb(int newProb){
    //     ghostEventProbability = newProb;
    // }
    // public static void setIceProb(int newProb){
    //     iceEventProbability = newProb;
    // }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
        int windEventProbability = 100;
        int ghostEventProbability = 100;
        int iceEventProbability = 100;

    public static GameManager instance;
    public GameState state;
    void Awake(){
        instance =  this;
    }

    void Start(){
        UpdateGameState(GameState.Phase1);
    }

    public void UpdateGameState(GameState newState){
        state = newState;

        switch (newState){
            case GameState.Phase1:
                HandlePhase1();
                break;
            case GameState.Phase2:
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
    public void HandlePhase1(){





    }
    public enum GameState {
        Phase1,
        Phase2,
        Phase3,
        Victory,
        GameOver
    }

//Probability Method
    private void EventProbabilityCounter(float multiplier){
        int numToCheck = 0;

        int windNumber = Random.Range(0 , windEventProbability);
        int ghostNumber = Random.Range(0, ghostEventProbability);
        int iceNumber = Random.Range(0, iceEventProbability);

        if(numToCheck == windNumber){
            //run wind event

        } else if(numToCheck == ghostNumber){
            //run ghost event

        } else if(numToCheck == iceNumber){
            //run ice event

        } else {
            //do nothing
        }

    }

    //EventMethods
    private void StartWindEvent(){
        Debug.Log("STARTED WIND");
        windEventProbability = 100;
    }
    private void StartGhostEvent(){
        Debug.Log("STARTED GHOST");
        ghostEventProbability = 100;
    }
    private void StartIceEvent(){
        Debug.Log("STARTED ICE");
        iceEventProbability = 100;
    }


//Coroutines 
    private IEnumerator IncreaseProbability(int eventProb){
        yield return new WaitForSeconds(1);
        eventProb--;
    }
}

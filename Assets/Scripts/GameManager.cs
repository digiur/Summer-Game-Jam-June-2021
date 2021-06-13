using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public GameState state;
    void Awake(){
        instance =  this;
    }

    void Start(){
        UpdateGameState(GameState.MainMenu);
    }

    public void UpdateGameState(GameState newState){
        state = newState;

        switch (newState){
            case GameState.MainMenu:
                HandleMainMenu();
                break;
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

    public void HandleMainMenu(){

    }
    public void HandlePhase1(){
        
    }
    public enum GameState {
        MainMenu,
        Phase1,
        Phase2,
        Phase3,
        Victory,
        GameOver
    }
}

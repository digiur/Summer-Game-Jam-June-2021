using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NavigateToMainMenu : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    public TransitionController tc;
    //Detect if a click occurs
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        tc.Transition("MainMenu");
    }
}

using UnityEngine;
using UnityEngine.EventSystems;

public class PlayButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private TransitionController tc;
    //Detect if a click occurs
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        tc.Transition("Plains");
    }
}
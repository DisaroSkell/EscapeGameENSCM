using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Stack_of_Books_1 : MonoBehaviour, IPointerClickHandler
{

    public GameObject button;

    //Detect if a click occurs
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        //Output to console the clicked GameObject's name and the following message. You can replace this with your own actions for when clicking the GameObject.
        Debug.Log(" Game Object Clicked!");
        ToggleVisibilityButtonSearch();
        
    }

    public void ToggleVisibilityButtonSearch(){
        if(button.activeSelf){
            button.SetActive(false);
        }
        else{
            button.SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Books_1
{
public class Stack_of_Books_1 : MonoBehaviour, IPointerClickHandler
{

    public GameObject button;
    public GameObject Popup1;
    public GameObject Popup2;
    public static int QRcode = 1;

    //Detect if a click occurs
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        //Output to console the clicked GameObject's name and the following message. You can replace this with your own actions for when clicking the GameObject.
        Debug.Log(" Game Object Clicked!");
        if (Popup1.activeSelf == false && Popup2.activeSelf == false){
            ToggleVisibilityButtonSearch();
        }
        
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
}
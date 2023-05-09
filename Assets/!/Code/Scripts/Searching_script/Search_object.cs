using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SearchObject
{
public class Search_object : MonoBehaviour, IPointerClickHandler
{

    public GameObject button;
    public GameObject Popup_found;
    public GameObject obj;
    public bool QRcode;
    public bool Phone;
    public static bool QR;
    public static bool phone;
    public static string nameobj = "";
    

    //Detect if a click occurs
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        QR = QRcode;
        phone = Phone;
        nameobj = obj.name;
        //Output to console the clicked GameObject's name and the following message. You can replace this with your own actions for when clicking the GameObject.
        if (Popup_found.activeSelf == false){
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
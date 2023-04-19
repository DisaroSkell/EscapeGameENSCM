using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Books_1;

public class Search_button : MonoBehaviour
{
    public GameObject button;
    public GameObject Popup_QRcode;
    public GameObject Popup_Nothing;

    public void ToggleVisibilityButton(){
        button.SetActive(false);
    }

    public void ToggleVisibilityPopup(){
        if (Stack_of_Books_1.QRcode == 1){
            if (Popup_QRcode.activeSelf){
                Popup_QRcode.SetActive(false);
                Stack_of_Books_1.QRcode = 0;
                }
            else{
                Popup_QRcode.SetActive(true);
            }
        }
        
        else{
            if (Popup_Nothing.activeSelf){
                Popup_Nothing.SetActive(false);
            }
            else{
                Popup_Nothing.SetActive(true);
            }
        }
    }

}

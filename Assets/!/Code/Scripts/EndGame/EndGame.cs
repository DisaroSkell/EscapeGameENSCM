using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

public class EndGame : MonoBehaviour, IPointerClickHandler
{

    public GameObject EndPopup;
    public GameObject ReturnButton;
    public GameObject InventoryButton;
    public GameObject NotesButton;
    public GameObject Timer;
    public TextMeshProUGUI TextTimer;
    public Timer final;
    

    [SerializeField] public UnityEvent OnPointerClickEvent = new UnityEvent();

    public void OnPointerClick(PointerEventData eventData) {
        OnPointerClickEvent.Invoke();
        SetFinalTimer();
        ToggleVisibilityEnd();
    } 

    public void ToggleVisibilityEnd(){
        if(EndPopup.activeSelf){
            EndPopup.SetActive(false);
            InventoryButton.SetActive(true);
            NotesButton.SetActive(true);
            Timer.SetActive(true);
        }
        else{
            EndPopup.SetActive(true);
            ReturnButton.SetActive(false);
            InventoryButton.SetActive(false);
            NotesButton.SetActive(false);
            Timer.SetActive(false);
        }
    }
    public void SetFinalTimer(){
        string time = final.GetElapsedTime();
        TextTimer.text = "Votre temps : " + time;
    }
}

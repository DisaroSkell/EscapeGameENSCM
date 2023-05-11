using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class EndGame : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] public UnityEvent OnPointerClickEvent = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData) {
        OnPointerClickEvent.Invoke();
    } 
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

/* Desk with a padlock.
Allows the user to try opening it by clicking on it. */
public class UnlockableDesk : MonoBehaviour, UnlockableIf, IPointerClickHandler {
    public PadlockInteraction padlock;

     [SerializeField] UnityEvent m_MyEvent;

    public bool IsLocked() {
        return padlock.gameObject.activeSelf;
    }

    public void Unlock() {
        padlock.gameObject.SetActive(false);
    }

    public void TryOpen() {
        if (this.IsLocked()) {
            Debug.Log("Desk is locked !");
        } else {
            m_MyEvent.Invoke();
        }
    }

    public void OnPointerClick(PointerEventData eventData) {
        this.TryOpen();
    }
}

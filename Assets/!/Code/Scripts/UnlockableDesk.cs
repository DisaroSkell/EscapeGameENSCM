using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnlockableDesk : MonoBehaviour, UnlockableIf, IPointerClickHandler {
    public PadlockInteraction padlock;

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
            Debug.Log("Opening desk...");
        }
    }

    public void OnPointerClick(PointerEventData eventData) {
        this.TryOpen();
    }
}

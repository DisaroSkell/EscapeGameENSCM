using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/* Class for lock interactions */
public abstract class LockInteractions : MonoBehaviour, IPointerClickHandler {
    // Code to unlock the lock
    public string code;

    public GameObject unlockUiParent;

    public Openable openable;

    /// <summary>
    /// Confirms the current try. 
    /// If the try is correct, this will unlock.
    /// </summary>
    public virtual void ConfirmTry(string currentTry) {
        if (this.code == currentTry && openable is not null) {
            this.openable.Open();
        }
    }

    /// <summary>
    /// Function to check if the lock is still locking the object.
    /// </summary>
    public abstract bool IsUnlocked();

    /// <summary>
    /// Opens the UI to try unlocking this.
    /// </summary>
    public abstract void OpenUnlockUI();

    /// <summary>
    /// Instantiates an unlock screen UI when the user clicks on it.
    /// Function of the IPointerClickHandler interface.
    /// </summary>
    /// <param name="PointerEventData">Unity class that contains information about a pointer event</param>
    public void OnPointerClick(PointerEventData eventData) {
        if (!this.IsUnlocked()) {
            this.OpenUnlockUI();
        }
    }
}
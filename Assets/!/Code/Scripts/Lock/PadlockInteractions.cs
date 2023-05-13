using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/* This class handles the interaction with a padlock.
It opens an unlock screen when clicked.
The user can then input a code by rotating numbers up and down. */
public class PadlockInteractions : LockInteractions {
    public char[] currentTry;

    public PadlockUnlockScreen unlockUIPrefab;

    // All alternatives for the current padlock.
    public string pickerAlternatives = "0123456789";

    /// <summary>
    /// Confirms the current try. 
    /// If the try is correct, the object desactivates.
    /// </summary>
    public void ConfirmTry() {
        bool result = this.code == new string (this.currentTry);

        if (result) {
            this.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Here, a padlock is locking an object if it is active.
    /// </summary>
    public override bool IsUnlocked() {
        return !this.gameObject.activeSelf;
    }

    void Start() {
        currentTry = new char[code.Length];

        for (int i = 0; i < code.Length; i++) {
            currentTry[i] = pickerAlternatives[0];
        }
    }

    /// <summary>
    /// This function rotates up the value of the current try at the given index.
    /// It takes the current value and puts the next value in the list of alternatives.
    /// </summary>
    /// <param name="index">Index of the element in the currentTry to rotate up.</param>
    public void RotateUpAt(int index) {
        // Next value is at the next index in the pickerAlternatives (the mod is in case we overflow the array)
        int newValueIndex = (pickerAlternatives.IndexOf(this.currentTry[index]) + 1) % pickerAlternatives.Length;

        this.currentTry[index] = pickerAlternatives[newValueIndex];
    }

    /// <summary>
    /// This function rotates down the value of the current try at the given index.
    /// It takes the current value and puts the previous value in the list of alternatives.
    /// </summary>
    /// <param name="index">Index of the element in the currentTry to rotate down.</param>
    public void RotateDownAt(int index) {
        // Previous value is at the previous index in the pickerAlternatives (we add the length because we don't want to go in the negatives)
        int newValueIndex = (pickerAlternatives.IndexOf(this.currentTry[index]) + pickerAlternatives.Length - 1) % pickerAlternatives.Length;

        this.currentTry[index] = pickerAlternatives[newValueIndex];
    }

    public override void OpenUnlockUI() {
        PadlockUnlockScreen unlockUI = (PadlockUnlockScreen)Instantiate(this.unlockUIPrefab, unlockUiParent.transform.position, Quaternion.identity, unlockUiParent.transform);
        unlockUI.Initialize(this);

        unlockUI.gameObject.SetActive(true);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadlockUnlockScreen : UnlockScreen<PadlockInteractions> {
    private PadlockInteractions padlock;

    public DigitPicker digitPickerUI;

    private DigitPicker[] pickers;

    /// <summary>
    /// Function to call as a constructor just after instantiation.
    /// </summary>
    /// <param name="PadlockInteractions">Padlock linked to the Unlock Screen.</param>
    public override void Initialize(PadlockInteractions padlock) {
        this.padlock = padlock;

        // UI inits

        this.pickers = new DigitPicker[padlock.code.Length];

        // Distance between two pickers. Calculated from the number of pickers and the UI size.
        double distBetweenPickers = (Screen.width * 0.5)/(padlock.code.Length - 1);

        // Number of distance between pickers we have from leftmost picker to the center.
        double distXFromCenter = padlock.code.Length%2 == 0 ? (padlock.code.Length/2 - 0.5) * (-1) : (padlock.code.Length/2) * (-1);

        for (int i = 0; i < padlock.code.Length; i++) {
            Vector3 coords = new Vector3((float)(this.transform.position.x + (distXFromCenter+i)*distBetweenPickers), this.transform.position.y);

            this.pickers[i] = (DigitPicker)Instantiate(digitPickerUI, coords, Quaternion.identity, this.transform);
            this.pickers[i].Initialize(padlock.currentTry[i], this);
        }
    }

    /// <summary>
    /// This function rotates up a digit of the padlock.
    /// It then updates the displayed digit based on the change that occured on the padlock.
    /// </summary>
    /// <param name="DigitPicker">Picker that needs a rotation.</param>
    public void RotateUpPicker(DigitPicker picker) {
        int index = Array.IndexOf(pickers, picker);

        if (index >= 0 && index < padlock.currentTry.Length) {
            padlock.RotateUpAt(index);
            picker.UpdateDigit(padlock.currentTry[index]);
        }
    }

    /// <summary>
    /// This function rotates down a digit of the padlock.
    /// It then updates the displayed digit based on the change that occured on the padlock.
    /// </summary>
    /// <param name="DigitPicker">Picker that needs a rotation.</param>
    public void RotateDownPicker(DigitPicker picker) {
        int index = Array.IndexOf(pickers, picker);

        if (index >= 0 && index < padlock.currentTry.Length) {
            padlock.RotateDownAt(index);
            picker.UpdateDigit(padlock.currentTry[index]);
        }
    }

    /// <summary>
    /// Function that checks if the current try matches the code of the padlock.
    /// It either deactivates the padlock or displays an incorrect code message.
    /// </summary>
    public void ConfirmTry() {
        if (padlock.code == new string (padlock.currentTry)) {
            Debug.Log("Correct code");
            padlock.gameObject.SetActive(false);
            this.CloseWindow();
        } else {
            Debug.Log("Incorrect code");
        }
    }
}
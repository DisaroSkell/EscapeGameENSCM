using System; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockScreen : MonoBehaviour {
    private PadlockInteraction padlock;

    public DigitPicker digitPickerUI;

    private DigitPicker[] pickers;

    public void Initialize(PadlockInteraction padlock) {
        this.padlock = padlock;

        // UI inits
        this.pickers = new DigitPicker[padlock.code.Length];
        double distXFromCenter = padlock.code.Length%2 == 0 ? (padlock.code.Length/2 - 0.5) * (-1) : (padlock.code.Length/2) * (-1);
        for (int i = 0; i < padlock.code.Length; i++) {
            Vector3 coords = new Vector3((float)(this.transform.position.x + (distXFromCenter+i)*100), this.transform.position.y);

            this.pickers[i] = (DigitPicker)Instantiate(digitPickerUI, coords, Quaternion.identity, this.transform);
            this.pickers[i].Initialize(padlock.currentTry[i], this);
        }
    }

    public void RotateUpPicker(DigitPicker picker) {
        int index = Array.IndexOf(pickers, picker);

        if (index >= 0 && index < padlock.currentTry.Length) {
            padlock.RotateUpAt(index);
            picker.UpdateDigit(padlock.currentTry[index]);
        }
    }

    public void RotateDownPicker(DigitPicker picker) {
        int index = Array.IndexOf(pickers, picker);

        if (index >= 0 && index < padlock.currentTry.Length) {
            padlock.RotateDownAt(index);
            picker.UpdateDigit(padlock.currentTry[index]);
        }
    }

    public void ConfirmTry() {
        if (padlock.code == new string (padlock.currentTry)) {
            Debug.Log("Correct code");
            padlock.gameObject.SetActive(false);
            Destroy(this.gameObject);
        } else {
            Debug.Log("Incorrect code");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/* The DigitPicker class displays a given digit that can be "rotated" up or down by its parent UnlockScreen. */
public class DigitPicker : MonoBehaviour {
    [SerializeField]
    private TextMeshProUGUI textMeshPro;

    private UnlockScreen parent;

    /// <summary>
    /// Function to call as a constructor just after instantiation.
    /// </summary>
    /// <param name="digit">Digit to be displayed on the screen.</param>
    /// <param name="UnlockScreen">The parent UnlockScreen.</param>
    public void Initialize(char digit, UnlockScreen parent) {
        this.textMeshPro.text = digit.ToString();
        this.parent = parent;
    }

    // Text setter
    public void UpdateDigit(char digit) {
        this.textMeshPro.text = digit.ToString();
    }

    // Delegation
    public void RotateUp() {
        this.parent.RotateUpPicker(this);
    }

    // Delegation
    public void RotateDown() {
        this.parent.RotateDownPicker(this);
    }
}

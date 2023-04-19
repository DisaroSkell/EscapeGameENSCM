using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DigitPicker : MonoBehaviour {
    [SerializeField]
    private TextMeshProUGUI textMeshPro;

    private UnlockScreen parent;

    public void Initialize(char digit, UnlockScreen parent) {
        this.textMeshPro.text = digit.ToString();
        this.parent = parent;
    }

    public void UpdateDigit(char digit) {
        this.textMeshPro.text = digit.ToString();
    }

    public void RotateUp() {
        this.parent.RotateUpPicker(this);
    }

    public void RotateDown() {
        this.parent.RotateDownPicker(this);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DigitPicker : MonoBehaviour {
    [SerializeField]
    private TextMeshProUGUI textMeshPro;

    public void Initialize(char digit) {
        textMeshPro.text = digit.ToString();
    }

    // TODO call parent function to rotate (+/-)
}

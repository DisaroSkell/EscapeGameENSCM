using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadlockInteraction : MonoBehaviour {
    public int[] code;

    public int[] currentTry;

    // TODO : use this
    public string pickerAlternatives = "0123456789";

    // TODO : Remove this
    bool IsCodeCorrect() {
        bool correct = true;
        int i = 0;

        while(i < code.Length && correct) {
            correct = currentTry[i] == code[i];
            i++;
        }

        return correct;
    }

    void Start() {
        currentTry = new int[code.Length];

        for (int i = 0; i < code.Length; i++)
        {
            currentTry[i] = 0;
        }
    }

    // TODO : Update this => open the unlock screen
    void OnMouseUpAsButton() {
        bool correctCode = IsCodeCorrect();
        if (correctCode) {
            Debug.Log("Correct code");
            this.gameObject.SetActive(false);
        } else {
            Debug.Log("Incorrect code");
        }
    }
}

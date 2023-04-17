using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockScreen : MonoBehaviour {
    public PadlockInteraction padlock;

    void RotateUpAt(int index) {
        padlock.currentTry[index]++;
    }

    void RotateDownAt(int index) {
        padlock.currentTry[index]--;
    }

    bool IsCodeCorrect() {
        bool correct = true;
        int i = 0;

        while(i < padlock.code.Length && correct) {
            correct = padlock.currentTry[i] == padlock.code[i];
            i++;
        }

        return correct;
    }

    void confirmTry() {
        bool correctCode = IsCodeCorrect();
        if (correctCode) {
            Debug.Log("Correct code");
            padlock.gameObject.SetActive(false);
        } else {
            Debug.Log("Incorrect code");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PadlockInteraction : MonoBehaviour, IPointerClickHandler {
    public string code;

    public char[] currentTry;

    public string pickerAlternatives = "0123456789";

    public GameObject unlockUiParent;

    public UnlockScreen unlockUiPrefab;

    private UnlockScreen unlockUI;

    bool IsCodeCorrect(string attempt) {
        return code == attempt;
    }

    void Start() {
        currentTry = new char[code.Length];

        for (int i = 0; i < code.Length; i++) {
            currentTry[i] = pickerAlternatives[0];
        }
    }

    public void RotateUpAt(int index) {
        // Next value is at the next index in the pickerAlternatives (the mod is in case we overflow the array)
        int newValueIndex = (pickerAlternatives.IndexOf(this.currentTry[index]) + 1) % pickerAlternatives.Length;

        this.currentTry[index] = pickerAlternatives[newValueIndex];
    }

    public void RotateDownAt(int index) {
        // Previous value is at the previous index in the pickerAlternatives (we add the length because we don't want to go in the negatives)
        int newValueIndex = (pickerAlternatives.IndexOf(this.currentTry[index]) + pickerAlternatives.Length - 1) % pickerAlternatives.Length;

        this.currentTry[index] = pickerAlternatives[newValueIndex];
    }

    // TODO : Update this => open the unlock screen
    public void OnPointerClick(PointerEventData eventData) {
        /* if (IsCodeCorrect(new string(currentTry))) {
            Debug.Log("Correct code");
            this.gameObject.SetActive(false);
        } else {
            Debug.Log("Incorrect code: " + new string(currentTry));
            this.RotateDownAt(0);
        } */

        this.unlockUI = (UnlockScreen)Instantiate(unlockUiPrefab, unlockUiParent.transform.position, Quaternion.identity, unlockUiParent.transform);
        unlockUI.Initialize(this);

        unlockUI.gameObject.SetActive(true);
    }
}

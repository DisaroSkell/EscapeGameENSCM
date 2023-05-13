using UnityEngine;
using UnityEngine.EventSystems;

// Class for interactions with the digicode.
public class DigicodeInteractions : LockInteractions {
    public DigicodeScreen unlockUIPrefab;

    private bool unlocked = false;

    /// <summary>
    /// Confirms the current try. 
    /// If the try is correct, the digicode will be unlocked.
    /// </summary>
    public void ConfirmTry(string currentTry) {
        bool result = this.code == currentTry;

        if (result) {
            this.unlocked = true;
        }
    }

    /// <summary>
    /// Here, a digicode is locking an object if it has its unlocked attribute at false.
    /// </summary>
    public override bool IsUnlocked() {
        return this.unlocked;
    }

    public override void OpenUnlockUI() {
        DigicodeScreen unlockUI = (DigicodeScreen)Instantiate(this.unlockUIPrefab, unlockUiParent.transform.position, Quaternion.identity, unlockUiParent.transform);
        unlockUI.Initialize(this);

        unlockUI.gameObject.SetActive(true);
    }
}
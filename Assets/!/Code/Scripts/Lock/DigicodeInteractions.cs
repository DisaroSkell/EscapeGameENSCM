using UnityEngine;

// Class for interactions with the digicode.
public class DigicodeInteractions : MonoBehaviour {
    public string code;

    public GameObject unlockUiParent;

    public DigicodeScreen unlockUIPrefab;

    private DigicodeScreen unlockUI;

    /// <summary>
    /// Opens the unlock UI by creating a prefab.
    /// </summary>
    /// <param name="unlockable">The unlockable object linked to the digicode.</param>
    public void OpenUnlockUI(Unlockable unlockable) {
        this.unlockUI = (DigicodeScreen)Instantiate(unlockUIPrefab, unlockUiParent.transform.position, Quaternion.identity, unlockUiParent.transform);
        unlockUI.Initialize(this, unlockable);

        unlockUI.gameObject.SetActive(true);
    }
}
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
    /// <param name="door">Door linked to the digicode.</param>
    public void OpenUnlockUI(UnlockableDoor door) {
        this.unlockUI = (DigicodeScreen)Instantiate(unlockUIPrefab, unlockUiParent.transform.position, Quaternion.identity, unlockUiParent.transform);
        unlockUI.Initialize(this, door);

        unlockUI.gameObject.SetActive(true);
    }
}
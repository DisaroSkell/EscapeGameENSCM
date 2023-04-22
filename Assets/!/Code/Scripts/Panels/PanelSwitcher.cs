using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelSwitcher : MonoBehaviour
{
    public GameObject ParentButton;

    /// <summary>
    /// This function sets all child buttons of a parent button to be interactable.
    /// </summary>
    public void SetAllButtonChildInteractable() {
        for (int i = 0; i < this.ParentButton.transform.childCount; i++) {
            this.ParentButton.transform.GetChild(i).gameObject.GetComponent<Button>().interactable = true;
        }
    }
}

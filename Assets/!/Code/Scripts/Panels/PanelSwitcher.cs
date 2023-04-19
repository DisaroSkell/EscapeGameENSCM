using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelSwitcher : MonoBehaviour
{
    public GameObject ParentPanel;
    public GameObject ParentButton;
    public GameObject NextPanel;

    /// <summary>
    /// The function switches to the next panel and sets all other panels and buttons to inactive or
    /// interactable.
    /// </summary>
    /// <returns>
    /// If the `NextPanel` is `null`, then the method will return without switching to the next panel
    /// </returns>
    public void SwitchPanel() {
        this.SetAllPanelChildInactive();

        this.SetAllButtonChildInteractable();

        if (this.NextPanel is null){return;}

        this.NextPanel.SetActive(true);
    }

    /// <summary>
    /// This function sets all child objects of a parent panel to be inactive.
    /// </summary>
    public void SetAllPanelChildInactive() {
        for (int i = 0; i < this.ParentPanel.transform.childCount; i++) {
            this.ParentPanel.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// This function sets all child buttons of a parent button to be interactable.
    /// </summary>
    public void SetAllButtonChildInteractable() {
        for (int i = 0; i < this.ParentButton.transform.childCount; i++) {
            this.ParentButton.transform.GetChild(i).gameObject.GetComponent<Button>().interactable = true;
        }
    }
}

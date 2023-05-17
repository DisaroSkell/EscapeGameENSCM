using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelOpener : MonoBehaviour
{

    public GameObject Panel;
    
    public void OpenPanel() {
        if (Panel is null){return;}
        try {
            Panel.SetActive(true);
        }
        catch(MissingReferenceException) {
        }

    }

    public void ClosePanel() {
        if (Panel is null){return;}

        try {
            Panel.SetActive(false);
        }
        catch(MissingReferenceException) {
        }
    }

    public void SwitchPanelDisplay() {
        if (Panel is null){return;}
        try {
            Panel.SetActive(!Panel.activeSelf);
        }
        catch(MissingReferenceException) {
        }
    }
}

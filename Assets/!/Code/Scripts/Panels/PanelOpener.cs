using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelOpener : MonoBehaviour
{

    public GameObject Panel;
    
    public void OpenPanel() {
        if (Panel is null){return;}

        Panel.SetActive(true);

    }

    public void ClosePanel() {
        if (Panel is null){return;}

        Panel.SetActive(false);
    }

    public void SwitchPanelDisplay() {
        if (Panel is null){return;}
        Panel.SetActive(!Panel.activeSelf);
    }
}

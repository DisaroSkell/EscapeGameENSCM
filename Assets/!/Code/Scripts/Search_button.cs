using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Search_button : MonoBehaviour
{
    public GameObject button;

    public void ToggleVisibility(){
        button.SetActive(false);
    }
}

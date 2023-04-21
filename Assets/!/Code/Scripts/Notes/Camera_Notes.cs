using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Notes : MonoBehaviour
{

    public GameObject camera_notes;
    public GameObject camera_Main;
    public GameObject canvas_notes;
    public GameObject Image_notes;

    public void ToggleVisibilityNotes(){
        if (canvas_notes.activeSelf){
            camera_notes.SetActive(false);
            canvas_notes.SetActive(false);
            Image_notes.SetActive(false);
            camera_Main.SetActive(true);
        }
        else{
            camera_notes.SetActive(true);
            canvas_notes.SetActive(true);
            Image_notes.SetActive(true);
            camera_Main.SetActive(false);
        }
    }
}

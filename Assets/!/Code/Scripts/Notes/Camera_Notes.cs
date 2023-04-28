using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Camera_Notes : MonoBehaviour
{

    public GameObject camera_notes;
    public GameObject camera_Main;
    public GameObject canvas_notes;
    public GameObject Image_notes;
    public GameObject clear_window;

    public void ToggleVisibilityNotes(){
        if (canvas_notes.activeSelf){
            this.DisableCamera(camera_notes);

            canvas_notes.SetActive(false);
            Image_notes.SetActive(false);

            this.EnableCamera(camera_Main);
        }
        else{
            this.EnableCamera(camera_notes);

            canvas_notes.SetActive(true);
            Image_notes.SetActive(true);

            this.DisableCamera(camera_Main);
        }
    }

    private void DisableCamera(GameObject camera) {
        camera.GetComponent<Camera>().enabled = false;

        PhysicsRaycaster raycaster = camera.GetComponent<PhysicsRaycaster>();
        
        if (raycaster) {
            raycaster.enabled = false;
        }
    }

    private void EnableCamera(GameObject camera) {
        camera.GetComponent<Camera>().enabled = true;

        PhysicsRaycaster raycaster = camera.GetComponent<PhysicsRaycaster>();
        
        if (raycaster) {
            raycaster.enabled = true;
        }
    }
    
    public void SelectClear()
    {
        clear_window.SetActive(true);
    }

    public void CloseClearWindow()
    {
        clear_window.SetActive(false);
    }

}

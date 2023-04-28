using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/* The CameraMovements class sets the focus of the camera using an animator. */
public class CameraMouvements : MonoBehaviour {
    public GameObject returnButtonRoom1;
    public GameObject returnButtonRoom2;
    private GameObject currentReturnButton;
    private CameraState state;

    public GameObject rooms;

    public CameraState State {
        get {
            return this.state;
        }
        // Changing the state changes the animator to make camera focus the right object
        set {
            if (this.state != value) {
                this.state = value;
                animator.SetInteger("Focus", (int)value);
            }
        }
    }

    public Animator animator;

    void Start() {
        this.State = CameraState.UnfocusedRoom1;
        this.currentReturnButton = this.returnButtonRoom1;
    }

    /// <summary>
    /// Only works with a state starting with "Unfocused".
    /// This function sets the state of the camera to an unfocused state.
    /// It desactivates the corresponding return button.
    /// It resets the colliders.
    /// </summary>
    private void Unfocus(CameraState unfocus) {
        if (unfocus == CameraState.UnfocusedRoom1 || unfocus == CameraState.UnfocusedRoom2) {
            this.State = unfocus;

            this.currentReturnButton.SetActive(false);

            // Disable all colliders
            FocusChanger.DisableCollidersFromChildren(rooms.transform);

            foreach (Transform room in this.rooms.transform) {
                // Reenable colliders of direct children of rooms (here colliders of POVs)
                FocusChanger.EnableCollidersFromDirectChildren(room);
            }
        }
    }

    /// <summary>
    /// Used by the return button.
    /// This function sets the state of the camera to UnfocusedRoom2.
    /// It desactivates the corresponding return button.
    /// It resets the colliders.
    /// </summary>
    public void UnfocusRoom1() {
        this.Unfocus(CameraState.UnfocusedRoom1);
    }

    /// <summary>
    /// Used by the return button.
    /// This function sets the state of the camera to UnfocusedRoom2.
    /// It desactivates the corresponding return button.
    /// It resets the colliders.
    /// </summary>
    public void UnfocusRoom2() {
        this.Unfocus(CameraState.UnfocusedRoom2);
    }

    public void ChangeRoom(TextMeshProUGUI textMesh) {
        if (this.State == CameraState.UnfocusedRoom1) {
            this.State = CameraState.UnfocusedRoom2;
            this.currentReturnButton = this.returnButtonRoom2;
            textMesh.text = "↓";
        } else if (this.State == CameraState.UnfocusedRoom2) {
            this.State = CameraState.UnfocusedRoom1;
            this.currentReturnButton = this.returnButtonRoom1;
            textMesh.text = "↑";
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/* The CameraMovements class sets the focus of the camera using an animator. */
public class CameraMouvements : MonoBehaviour {
    public GameObject returnButtonRoom1;
    public GameObject returnButtonRoom2;
    private GameObject currentReturnButton;
    private CameraState state;

    public GameObject rooms;

    public UnlockableDoor door;

    public GameObject changeRoomButton;

    public CameraState State {
        get {
            return this.state;
        }
        // Changing the state changes the animator to make camera focus the right object
        set {
            if (this.state != value) {
                this.state = value;
                animator.SetInteger("Focus", (int)value);

                if (value != CameraState.UnfocusedRoom1 && value != CameraState.UnfocusedRoom2) {
                    this.changeRoomButton.SetActive(false);
                }
            }
        }
    }

    public Animator animator;

    void Start() {
        this.State = CameraState.UnfocusedRoom1;
        this.currentReturnButton = this.returnButtonRoom1;
        this.changeRoomButton.SetActive(false);
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
            Utils.DisableCollidersFromChildren(rooms.transform);

            foreach (Transform room in this.rooms.transform) {
                // Reenable colliders of direct children of rooms (here colliders of POVs)
                Utils.EnableCollidersFromDirectChildren(room);
            }

            if (!this.door.IsLocked()) {
                this.changeRoomButton.SetActive(true);
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

    /// <summary>
    /// Changes the room the camera is focusing.
    /// </summary>
    /// <param name="textMesh">Text of the button to change.</param>
    public void ChangeRoom(TextMeshProUGUI textMesh) {
        if (this.State == CameraState.UnfocusedRoom1) {
            this.State = CameraState.UnfocusedRoom2;
            this.currentReturnButton = this.returnButtonRoom2;
            textMesh.text = "↓";
        } else if (this.State == CameraState.UnfocusedRoom2) {
            this.State = CameraState.UnfocusedRoom1;
            this.currentReturnButton = this.returnButtonRoom1;
            textMesh.text = "↑";
        } else {
            this.currentReturnButton.GetComponent<Button>().onClick.Invoke();
            this.ChangeRoom(textMesh);
        }
    }
}

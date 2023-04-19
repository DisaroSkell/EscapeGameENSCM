using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* The CameraMovements class sets the focus of the camera using an animator. */
public class CameraMouvements : MonoBehaviour {
    public GameObject returnButton;
    public GameObject rooms;

    public CameraState state {
        // Changing the state changes the animator to make camera focus the right object
        set {
            animator.SetInteger("Focus", (int)value);
        }
    }

    public Animator animator;

    void Start() {
        state = CameraState.Unfocused;
    }

    /// <summary>
    /// This function sets the state of the camera to Unfocused.
    /// Used by the Return button of Unity.
    /// </summary>
    public void ResetFocus() {
        this.state = CameraState.Unfocused;

        this.returnButton.SetActive(false);

        FocusChanger.EnableCollidersFromChildren(rooms.transform);
    }
}

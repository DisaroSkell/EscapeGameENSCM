using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* The CameraMovements class sets the focus of the camera using an animator. */
public class CameraMouvements : MonoBehaviour {
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
}

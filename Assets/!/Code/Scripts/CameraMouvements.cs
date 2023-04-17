using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMouvements : MonoBehaviour
{
    public CameraState state
    {
        set
        {
            animator.SetInteger("Focus", (int)value);
        }
    }

    public Animator animator;

    void Start()
    {
        state = CameraState.Unfocused;
    }
}

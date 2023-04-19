using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/* Class to change the camera state when the user clicks on it. */
public class FocusChanger : MonoBehaviour, IPointerClickHandler {
    public CameraState aim;

    public CameraMouvements camMove;

    public void OnPointerClick(PointerEventData eventData) {
        camMove.state = aim;
    }
}

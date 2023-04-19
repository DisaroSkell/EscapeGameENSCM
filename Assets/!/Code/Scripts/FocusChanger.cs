using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/* Class to change the camera state when the user clicks on it. */
public class FocusChanger : MonoBehaviour, IPointerClickHandler {
    public CameraState aim;

    public CameraMouvements camMove;

    public GameObject returnButton;

    public void OnPointerClick(PointerEventData eventData) {
        camMove.state = aim;
        returnButton.SetActive(true);

        EnableCollidersFromChildren(this.transform);
        DisableCollidersFromDirectChildren(this.transform.parent);
    }

    static public void DisableCollidersFromDirectChildren(Transform parent) {
        Collider[] colliders = parent.GetComponentsInChildren<Collider>();

        foreach (var collider in colliders) {
            if(collider.gameObject.transform.parent == parent) {
                collider.enabled = false;
            }
        }
    }

    static public void DisableCollidersFromChildren(Transform parent) {
        Collider[] colliders = parent.GetComponentsInChildren<Collider>();

        foreach (var collider in colliders) {
            collider.enabled = false;
        }
    }

    static public void EnableCollidersFromDirectChildren(Transform parent) {
        Collider[] colliders = parent.GetComponentsInChildren<Collider>();

        foreach (var collider in colliders) {
            if(collider.gameObject.transform.parent == parent) {
                collider.enabled = true;
            }
        }
    }

    static public void EnableCollidersFromChildren(Transform parent) {
        Collider[] colliders = parent.GetComponentsInChildren<Collider>();

        foreach (var collider in colliders) {
            collider.enabled = true;
        }
    }
}

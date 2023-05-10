using UnityEngine;
using System.Collections.Generic;

public class Utils {
    /// <summary>
    /// Gets all children of a game object, even inactive ones.
    /// </summary>
    /// <param name="parent">Parent we are searching in.</param>
    /// <returns>
    /// Returns the list of children as game objects.
    /// </returns>
    public static List<GameObject> GetAllChildren(GameObject parent) {
        List<GameObject> childrenGO = new List<GameObject>();

        Transform[] childrenTransform = parent.GetComponentsInChildren<Transform>(true);

        for (int i = 0; i < childrenTransform.Length; i++) {
            childrenGO.Add(childrenTransform[i].gameObject);
        }

        return childrenGO;
    }

    /// <summary>
    /// Disables all children in parent.
    /// </summary>
    /// <param name="parent">Parent we are searching in.</param>
    public static void DisableAllChildren(GameObject parent) {
        for (int i = 0; i< parent.transform.childCount; i++) {
            parent.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Disables all colliders of direct children in parent.
    /// </summary>
    /// <param name="parent">Parent we are searching in.</param>
    public static void DisableCollidersFromDirectChildren(Transform parent) {
        Collider[] colliders = parent.GetComponentsInChildren<Collider>();

        foreach (var collider in colliders) {
            if(collider.gameObject.transform.parent == parent) {
                collider.enabled = false;
            }
        }
    }

    /// <summary>
    /// Disables all colliders of children in parent.
    /// </summary>
    /// <param name="parent">Parent we are searching in.</param>
    public static void DisableCollidersFromChildren(Transform parent) {
        Collider[] colliders = parent.GetComponentsInChildren<Collider>();

        foreach (var collider in colliders) {
            collider.enabled = false;
        }
    }

    /// <summary>
    /// Enable all colliders of direct children in parent.
    /// </summary>
    /// <param name="parent">Parent we are searching in.</param>
    public static void EnableCollidersFromDirectChildren(Transform parent) {
        Collider[] colliders = parent.GetComponentsInChildren<Collider>();

        foreach (var collider in colliders) {
            if(collider.gameObject.transform.parent == parent) {
                collider.enabled = true;
            }
        }
    }

    /// <summary>
    /// Enable all colliders of children in parent.
    /// </summary>
    /// <param name="parent">Parent we are searching in.</param>
    public static void EnableCollidersFromChildren(Transform parent) {
        Collider[] colliders = parent.GetComponentsInChildren<Collider>();

        foreach (var collider in colliders) {
            collider.enabled = true;
        }
    }
}
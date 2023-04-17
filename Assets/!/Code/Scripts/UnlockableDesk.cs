using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockableDesk : MonoBehaviour, UnlockableIf {
    public PadlockInteraction padlock;

    public bool IsLocked() {
        return padlock.gameObject.activeSelf;
    }

    public void Unlock() {
        padlock.gameObject.SetActive(false);
    }
}

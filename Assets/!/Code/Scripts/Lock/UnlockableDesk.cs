using UnityEngine;

/* Desk with a padlock.
Allows the user to try opening it by clicking on it. */
public class UnlockableDesk : Unlockable {
    public PadlockInteraction padlock;

    public override bool IsLocked() {
        return this.padlock.gameObject.activeSelf;
    }

    public override void Unlock() {
        this.padlock.gameObject.SetActive(false);
    }

    public override void TryOpen() {
        if (this.IsLocked()) {
            Debug.Log("Desk is locked !");
        } else {
            this.m_MyEvent.Invoke();
        }
    }
}

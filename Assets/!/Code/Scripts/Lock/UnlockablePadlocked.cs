using UnityEngine;

/* Object locked with a padlock.
Allows the user to try opening it by clicking on it. */
public class UnlockablePadlocked : Unlockable {
    public PadlockInteractions padlock;

    public override bool IsLocked() {
        return this.padlock.gameObject.activeSelf;
    }

    public override void Unlock() {
        this.padlock.gameObject.SetActive(false);
    }
}

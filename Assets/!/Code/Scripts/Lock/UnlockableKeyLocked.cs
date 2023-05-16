using UnityEngine;

public class UnlockableKeyLocked : Unlockable {
    private bool locked = false;

    public override bool IsLocked() {
        return this.locked;
    }

    public override void Unlock() {
        this.locked = false;
    }
}
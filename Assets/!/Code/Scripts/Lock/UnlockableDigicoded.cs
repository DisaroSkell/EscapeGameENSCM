using UnityEngine;

/* Object locked with a digicode.
Allows the user to try opening it by clicking on it. */
public class UnlockableDigicoded : Unlockable {
    public DigicodeInteractions digicode;

    public override bool IsLocked() {
        return !this.digicode.IsUnlocked();
    }

    /// <summary>
    /// Here, unlocking the door means cracking the digicode.
    /// </summary>
    public override void Unlock() {
        this.digicode.ConfirmTry(this.digicode.code);
    }
}

using UnityEngine;

/* Safe locked with a digicode.
Allows the user to try opening it by clicking on it. */
public class UnlockableSafe : Unlockable {
    public DigicodeInteractions safeLock;

    public Animator safeAnimator;

    private bool isLocked;

    public void Start() {
        this.isLocked = true;
        safeAnimator.SetBool("IsOpened", false);
    }

    public override bool IsLocked() {
        return this.isLocked;
    }

    public override void Unlock() {
        this.safeLock.GetComponent<Collider>().enabled = false;
        this.isLocked = false;
        safeAnimator.SetBool("IsOpened", true);
    }

    public override void TryOpen() {
        if (this.IsLocked()) {
            this.safeLock.OpenUnlockUI(this);
        } else {
            this.m_MyEvent.Invoke();
        }
    }
}

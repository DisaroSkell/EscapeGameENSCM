using UnityEngine;

/* Door locked with a digicode.
Allows the user to try opening it by clicking on it. */
public class UnlockableDoor : Unlockable {
    public DigicodeInteractions doorLock;

    public Animator doorAnimator;

    private bool isLocked;

    public void Start() {
        this.isLocked = true;
        doorAnimator.SetBool("IsOpened", false);
    }

    public override bool IsLocked() {
        return this.isLocked;
    }

    public override void Unlock() {
        this.doorLock.GetComponent<Collider>().enabled = false;
        this.isLocked = false;
        doorAnimator.SetBool("IsOpened", true);
    }

    public override void TryOpen() {
        if (this.IsLocked()) {
            this.doorLock.OpenUnlockUI(this);
        } else {
            this.m_MyEvent.Invoke();
        }
    }
}

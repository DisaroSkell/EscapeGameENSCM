using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

// Class for interactions with the digicode.
public class DigicodeInteractions : LockInteractions {
    public DigicodeScreen unlockUIPrefab;

    // Number of try before the object locks definitively. -1 for infinite tries.
    public int maxNbTries;

    private bool unlocked = false;
    
    // Current number of tries.
    private int tryCount;

    [SerializeField] protected UnityEvent blockedEvents;

    public void Start() {
        if (this.maxNbTries == -1) {
            this.tryCount = -10;
        } else {
            this.tryCount = 0;
        }
    }

    public int GetTryCount() {
        return this.tryCount;
    }

    /// <summary>
    /// Confirms the current try.
    /// If we try too many times, it blocks and invokes blockedEvents.
    /// If the try is correct, the digicode will be unlocked.
    /// </summary>
    /// <param name="string">Current try to unlock the digicode.</param>
    public override void ConfirmTry(string currentTry) {
        if (this.tryCount >= this.maxNbTries) {
            this.blockedEvents.Invoke();
        } else if (this.code == currentTry) {
            this.unlocked = true;
            base.ConfirmTry(currentTry);
        }

        this.IncrementTryCount();
    }

    /// <summary>
    /// Here, a digicode is locking an object if it has its unlocked attribute at false.
    /// </summary>
    public override bool IsUnlocked() {
        return this.unlocked;
    }

    /// <summary>
    /// Increments the count of tries by 1. Only works if the number of try is finite.
    /// </summary>
    public void IncrementTryCount() {
        if (this.tryCount >= 0 ) {
            this.tryCount++;
        }
    }

    /// <summary>
    /// Resets the count of tries to 0.
    /// </summary>
    public void ResetTryCount() {
        this.tryCount = 0;
    }

    /// <summary>
    /// Blocks the digicode and calls the blockedEvents.
    /// </summary>
    public void BlockDigicode() {
        this.unlocked = false;
        this.tryCount = this.maxNbTries;

        this.ConfirmTry("");
    }

    public override void OpenUnlockUI() {
        DigicodeScreen unlockUI = (DigicodeScreen)Instantiate(this.unlockUIPrefab, unlockUiParent.transform.position, Quaternion.identity, unlockUiParent.transform);
        unlockUI.Initialize(this);

        unlockUI.gameObject.SetActive(true);
    }
}
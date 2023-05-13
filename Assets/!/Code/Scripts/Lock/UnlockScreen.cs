using System; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* UnlockScreen class to manage UI elements for lock interactions.
Allows the user to unlock an object. */
public abstract class UnlockScreen<Lock> : MonoBehaviour where Lock : LockInteractions {
    private Lock lockObj;

    /// <summary>
    /// Function to call as a constructor just after instantiation.
    /// </summary>
    /// <param name="Lock">Lock linked to the Unlock Screen.</param>
    public virtual void Initialize(Lock lockObj) {
        this.lockObj = lockObj;
    }

    /// <summary>
    /// Closes the UI window and tells the lock it is linked to that it did.
    /// </summary>
    public void CloseWindow() {
        this.lockObj.SetUIOpened(false);
        Destroy(this.gameObject);
    }
}

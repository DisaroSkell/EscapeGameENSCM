using System; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* UnlockScreen class to manage UI elements for lock interactions.
Allows the user to unlock an object. */
public abstract class UnlockScreen<Lock> : MonoBehaviour where Lock : LockInteractions {
    /// <summary>
    /// Function to call as a constructor just after instantiation.
    /// </summary>
    /// <param name="Lock">Lock linked to the Unlock Screen.</param>
    public abstract void Initialize(Lock lockObj);

    public void CloseWindow() {
        Destroy(this.gameObject);
    }
}

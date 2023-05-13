using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

/* Interface for unlockable objects. */
public abstract class Unlockable : MonoBehaviour, IPointerClickHandler {
    // Event list to use when triggered.
    [SerializeField] protected UnityEvent m_OpenEvents;

    public abstract bool IsLocked();

    public abstract void Unlock();

    public void TryOpen() {
        if (this.IsLocked()) {
            print("It is locked !");
        } else {
            this.m_OpenEvents.Invoke();
        }
    }

    public void OnPointerClick(PointerEventData eventData) {
        this.TryOpen();
    }
}
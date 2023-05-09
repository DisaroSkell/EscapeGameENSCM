using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

/* Interface for unlockable objects. */
public abstract class Unlockable : MonoBehaviour, IPointerClickHandler {
    // Event list to use when triggered.
    [SerializeField] protected UnityEvent m_MyEvent;

    public abstract bool IsLocked();

    public abstract void Unlock();

    public abstract void TryOpen();

    public void OnPointerClick(PointerEventData eventData) {
        this.TryOpen();
    }
}
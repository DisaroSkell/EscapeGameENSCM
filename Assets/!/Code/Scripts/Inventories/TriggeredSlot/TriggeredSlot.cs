using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggeredSlot : MonoBehaviour
{
    public UnityEvent trigger;

    public void DeleteGameObject(GameObject obj) {
        if(obj is null) return;
        Destroy(obj);
    }

}

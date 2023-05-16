using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggeredSlot : MonoBehaviour
{
    public UnityEvent trigger;

    public void DeleteGameObjectPanel(GameObject panel) {
        if(panel is null) return;
        Destroy(panel);
    }

}

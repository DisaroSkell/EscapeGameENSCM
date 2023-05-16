using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName= "New Triggered Object", menuName = "Inventory System/Items/Triggered")]
public class TriggeredObject : ItemObject
{

    public ItemObject triggerObject;
   public void Awake() {
        type = ItemType.Triggered;
   }
}

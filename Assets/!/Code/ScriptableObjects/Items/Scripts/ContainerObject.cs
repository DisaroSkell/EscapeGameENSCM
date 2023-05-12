using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName= "New Container Object", menuName = "Inventory System/Items/Container")]
// a Container object is an object that open an inventory when clicked in a inventory
public class ContainerObject : ItemObject
{
    public GameObject inventoryToOpen;
    public void Awake() {
        type = ItemType.Container;
   }
}

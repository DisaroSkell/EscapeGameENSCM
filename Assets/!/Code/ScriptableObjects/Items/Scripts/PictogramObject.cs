using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName= "New Pictogram Object", menuName = "Inventory System/Items/Pictogram")]

public class PictogramObject : ItemObject
{
    public void Awake() {
        type = ItemType.Pictogram;
    }
}

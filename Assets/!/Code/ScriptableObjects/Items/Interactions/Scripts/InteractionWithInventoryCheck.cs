using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName= "New Interaction With Inv Check", menuName = "Inventory System/Items/InteractionWithInventoryCheck")]
public class InteractionWithInventoryCheck : Interaction
{
    public Inventory inventoryToCheck;
    public Inventory solution;
}

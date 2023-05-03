using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName= "New Interaction", menuName = "Inventory System/Items/Interaction")]
public class Interaction : ScriptableObject {
    public ItemObject draggedItem;
    public ItemObject fixedItem;

    public bool destroyDraggedObject;
    public bool destroyFixedObject;

    public bool worksBothWays;

    public List<ItemObject> itemsToAdd;

    public void MakeIteraction() {
        if(destroyDraggedObject) draggedItem.inventory.RemoveItem(draggedItem);
        if(destroyFixedObject) fixedItem.inventory.RemoveItem(fixedItem);
        AddAllObject();
    }

    private void DestroyObject(GameObject obj, ItemObject item) {
        item.inventory.RemoveItem(item);
        Destroy(obj);
    }

    private void AddAllObject() {
        foreach (var item in itemsToAdd) {
            item.inventory.AddItem(item);
        }
    }

#nullable enable
    public static bool isThereInteractionBetween(ItemObject draggedItem, ItemObject fixedItem) {
        Interaction? draggedItemInteraction = draggedItem.interaction;
        Interaction? fixedItemInteraction = fixedItem.interaction;
        if(draggedItemInteraction is null) {
            // no interaction at all
            if(fixedItemInteraction is null) return false;
            // only one way interaction (dragged on fixed)
            if(!fixedItemInteraction.worksBothWays) return false;
            // return other way possible
            return isThereInteractionBetween(fixedItem, draggedItem);
        }
        return draggedItemInteraction.draggedItem == draggedItem && draggedItemInteraction.fixedItem == fixedItem;



    }
}

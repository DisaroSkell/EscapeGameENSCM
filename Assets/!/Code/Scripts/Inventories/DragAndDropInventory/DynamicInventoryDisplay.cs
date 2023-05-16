using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

#nullable enable
public class DynamicInventoryDisplay : AbstractDragAndDropInventoryDisplay {

    public int X_START;
    public int Y_START;
    public int X_SPACE_BETWEEN_ITEMS;
    public int NUMBER_OF_CULUMN;
    public int Y_SPACE_BETWEEN_ITEMS;

    public bool takeInventory;

    #nullable disable
    public GameObject parentCanvas;
    #nullable enable
    public override void Display() {
        for (int i = 0; i < inventory.Length; i++) {
            InstantiateObject(i);
        }
    }

    private void InstantiateObject(int i) {
        ItemObject? item = inventory.GetItem(i);
        GameObject prefab;
        
        if(item is null) {
            prefab = EmptySlot;
        }
        else {
            prefab = item.prefab;
        }

        // create a new object with item.prefab and set it at the good position in the inventory
        GameObject obj = Instantiate(prefab, Vector3.zero, Quaternion.identity, transform);
        obj.transform.SetParent(panel.transform);
        obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
        this.objectList.Add(obj);

        if(this.takeInventory) {
            if(item is not null) {
                SetObjPickable(obj, item);
            }
        }
        else if (item is not null && item.GetType() == typeof(DocumentObject)) {
            SetObjViewable(obj, (DocumentObject)item);
        }
        else if (item is not null && item.type == ItemType.Container) {
            SetObjContainerOpener(obj, (ContainerObject)item);
        }
        else {
            LinkDragEvents(obj);
        }
    }

    private void SetObjPickable(GameObject obj, ItemObject itemObject) {
        // collider to interact with the object
        BoxCollider2D bc = obj.AddComponent<BoxCollider2D>();
        // attached item to the object
        Item newItem = obj.AddComponent<Item>();
        newItem.item = itemObject;
        newItem.inventory = itemObject.inventory;
        // add event on click to remove item from the TakeInventory
        UnityAction handleItemClickedUnityAction = () => { this.HandleItemClicked(newItem); };
        newItem.OnPointerClickEvent.AddListener(handleItemClickedUnityAction);
        // add event to destroy item and set it into the new inventory
        newItem.OnPointerClickEvent.AddListener(newItem.PickUpItem);
    }

    public void HandleItemClicked(Item item) {
        this.inventory.RemoveItem(item.item);
    }

    private void SetObjViewable(GameObject obj, DocumentObject itemObject) {
        // get the document generator object
        GenerateDocument documentGenerator = this.gameObject.GetComponent<GenerateDocument>();

        // set collider to interact with the object 
        BoxCollider2D bc = obj.AddComponent<BoxCollider2D>();
        // attcahed item to the object
        Item newItem = obj.AddComponent<Item>();
        newItem.item = itemObject;
        newItem.inventory = itemObject.inventory;

        // set a panel opener to open the document panel when document is clicked
        PanelOpener panelOpener = obj.AddComponent<PanelOpener>();
        panelOpener.Panel = documentGenerator.documentViewer;
        newItem.OnPointerClickEvent.AddListener(panelOpener.OpenPanel);

        // set images to Document Generator and display images in the event
        UnityAction handleDocumentClickedUnityAction = () => { 
            documentGenerator.folderRessourcesName = itemObject.folderRessourcesName;
            documentGenerator.Display();
        };
        newItem.OnPointerClickEvent.AddListener(handleDocumentClickedUnityAction);
    }

    private void SetObjContainerOpener(GameObject obj, ContainerObject itemObject) {
        // collider to interact with the object
        BoxCollider2D bc = obj.AddComponent<BoxCollider2D>();
        // attached item to the object
        Item newItem = obj.AddComponent<Item>();
        newItem.item = itemObject;
        newItem.inventory = itemObject.inventory;

        // PanelOpener opener = obj.AddComponent<PanelOpener>();
        // opener.Panel = this.GetComponentInParent<Transform>().gameObject;
        UnityAction handleItemClickedUnityAction = () => { Instantiate(itemObject.inventoryToOpen, this.parentCanvas.transform.position, Quaternion.identity, this.parentCanvas?.transform); };

        // add event on click to open panel
        newItem.OnPointerClickEvent.AddListener(handleItemClickedUnityAction);
    }

    /// <summary>
    /// The function returns a Vector3 position based on the index i and predefined constants.
    /// </summary>
    /// <param name="i">The parameter "i" is an integer representing the index of the item for which we
    /// want to calculate the position.</param>
    /// <returns>
    /// A Vector3 object is being returned.
    /// </returns>
    private Vector3 GetPosition(int i) {
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEMS * (i%NUMBER_OF_CULUMN)), Y_START + (-Y_SPACE_BETWEEN_ITEMS * (i/NUMBER_OF_CULUMN)), 0f);
    }

    protected override void OnDragEndAction(GameObject obj)
    {
        PlayerMouse mouse = this.player.playerMouse;
        // GUARD: there is an item
        if(mouse.itemFrom is null) return;

        // GUARD: drag end on an inventory
        if(mouse.inventoryDisplayTo is null) return;
        
        // GUARD: indexes of items are set
        if(mouse.indexFrom < 0 || mouse.indexTo < 0) return;

        if(mouse.inventoryDisplayTo.GetType() == typeof(PlacementInventoryDisplay)) {
            EndDragOnPlacementInventory();
        }
        else if (mouse.inventoryDisplayTo == this) {
            EndDragOnSameInventory();
        }
    }

    private void EndDragOnSameInventory() {
        PlayerMouse mouse = this.player.playerMouse;

        // interaction code
        Debug.Log(
            string.Format("interaction between inventory[{0}] and inventory[{1}]", mouse.indexFrom, mouse.indexTo)
        );

        ItemObject? draggedItmObj = this.inventory.GetItem(mouse.indexFrom);
        ItemObject? fixedItmObj = this.inventory.GetItem(mouse.indexTo);
        if(fixedItmObj is not null && draggedItmObj is not null) {
            if(Interaction.isThereInteractionBetween(draggedItmObj, fixedItmObj)){
                Interaction? interaction = draggedItmObj.interaction;
                if(interaction is null ) {
                    interaction = fixedItmObj.interaction;
                }
                if(interaction is null) {
                    return; // can't arrive
                }
                interaction.MakeIteraction();
            }
        }

        
    }

    
    private void EndDragOnPlacementInventory() {
        PlayerMouse mouse = this.player.playerMouse;
        // GUARD: item has the same type as the inventory
        AbstractDragAndDropInventoryDisplay? inv = mouse.inventoryDisplayTo;
        if(inv is null) return;
        ItemObject? itemTo = inv.inventory.GetItem(mouse.indexTo);
        if(itemTo is not null) {
            if(itemTo.type != ItemType.Triggered) return;
            TriggeredObject triggeredObj = (TriggeredObject)itemTo;
            if(mouse.itemFrom != triggeredObj.triggerObject) return;

            GameObject objTo = inv.objectList[mouse.indexTo];
            objTo.GetComponent<TriggeredSlot>().trigger.Invoke();

        };
        ItemType? invType = inv.inventory.type;
        if(invType != ItemType.All && mouse.itemFrom?.type != invType) return;
        inv.inventory.SetItem(mouse.indexTo, mouse.itemFrom);

        // update other inventory display
        inv.UpdateDisplay();
        if(mouse.itemFrom is not null) {
            this.inventory.RemoveItem(mouse.itemFrom);
        }

    }

    public override void CreateDisplay()
    {
        this.UpdateDisplay();
    }
}

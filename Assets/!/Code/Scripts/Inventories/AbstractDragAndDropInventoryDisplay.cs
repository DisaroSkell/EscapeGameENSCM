using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class AbstractDragAndDropInventoryDisplay : AbstractInventoryDisplay
{
    public GameObject EmptySlot;

    #nullable enable

    // public Dictionary<GameObject, ItemObject> objectItem = new Dictionary<GameObject, ItemObject>();

    /// <summary>
    /// This function adds an event listener to a specified game object for a specified event trigger
    /// type.
    /// </summary>
    /// <param name="GameObject">The game object to which the event trigger component will be
    /// attached.</param>
    /// <param name="EventTriggerType">EventTriggerType is an enum that represents the type of event
    /// that will trigger the callback. It can be any of the following: PointerEnter, PointerExit,
    /// PointerDown, PointerUp, PointerClick, Drag, Drop, Scroll, UpdateSelected, Select, Deselect,
    /// Move, InitializePotentialDrag</param>
    /// <param name="action">The UnityAction<BaseEventData> parameter is a delegate that represents a
    /// method that takes a single parameter of type BaseEventData and returns void. It is used to
    /// specify the action that should be executed when the event is triggered.</param>
    /// <returns>
    /// The method does not return anything, it is a void method.
    /// </returns>
    protected void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action){
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        if(trigger == null) {
            return;
        }
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }

    /// <summary>
    /// This function adds drag events to a given game object.
    /// </summary>
    /// <param name="GameObject">A reference to a game object</param>
    protected void LinkDragEvents(GameObject obj) {
        AddEvent(obj, EventTriggerType.PointerEnter, delegate {OnEnter(obj);});
        AddEvent(obj, EventTriggerType.PointerExit, delegate {OnExit(obj);});
        AddEvent(obj, EventTriggerType.BeginDrag, delegate {OnDragStart(obj);});
        AddEvent(obj, EventTriggerType.EndDrag, delegate {OnDragEnd(obj);});
        AddEvent(obj, EventTriggerType.Drag, delegate {OnDrag(obj);});
    }

    protected abstract void OnDragEndAction(GameObject obj);
    protected void OnEnter(GameObject obj) {
        ItemObject? item = this.inventory.GetItem(this.player.playerMouse.indexTo);
        player.playerMouse.itemTo = item;
        this.player.playerMouse.indexTo = this.objectList.IndexOf(obj);
        player.playerMouse.inventoryDisplayTo = this;
        print(player.playerMouse.inventoryDisplayTo);
    }
    protected void OnDragEnd(GameObject obj) {
        Destroy(player.playerMouse.floatingObject);
        player.playerMouse.floatingObject = null;
        this.OnDragEndAction(obj);
        player.playerMouse.itemFrom = null;
        player.playerMouse.indexFrom = -1;
        this.UpdateDisplay();
    }

    /// <summary>
    /// The function clears the hover slot and object of the player's mouse item when an object exits.
    /// </summary>
    /// <param name="GameObject">The GameObject</param>
    protected void OnExit(GameObject obj) {
        player.playerMouse.itemTo = null;
        player.playerMouse.indexTo = -1;
        print("CACA");
        player.playerMouse.inventoryDisplayTo = null;
    }
    protected void OnDragStart(GameObject obj) {
        // GUARD: obj is not null
        if(obj is null) return;

        int itemIndex = this.objectList.IndexOf(obj);

        ItemObject? item = this.inventory.GetItem(itemIndex);

        // GUARD: item is not null
        if(item is null) return;
        
        // create floating object hover mouse
        var mouseObject = new GameObject();
        var rt = mouseObject.AddComponent<RectTransform>();
        mouseObject.transform.SetParent(obj.transform.parent, false);

        // adding texture of draged item to the object
        var img = mouseObject.AddComponent<RawImage>();
        img.texture = item.GetPrefab().GetComponent<RawImage>().texture;
        img.raycastTarget = false;
        // setting size of floating object
        RectTransform rtObj = item.GetPrefab().GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2((float)(rtObj.rect.width*0.8), (float)(rtObj.rect.height*0.8));
        rt.localScale = new Vector3(1, 1, 1);

        // set object to playerMouse
        player.playerMouse.floatingObject = mouseObject;
        player.playerMouse.itemFrom = item;
        player.playerMouse.indexFrom = itemIndex;
    }
    protected void OnDrag(GameObject obj) {
        if(obj is null) {return;}
        if(player.playerMouse.floatingObject is not null) {
            player.playerMouse.floatingObject.GetComponent<RectTransform>().position = Input.mousePosition;
        }
    }
}

public class PlayerMouse {
    public GameObject? floatingObject;
    public ItemObject? itemFrom;
    public ItemObject? itemTo;

    public int indexFrom = -1;
    public int indexTo = -1;

    public AbstractDragAndDropInventoryDisplay? inventoryDisplayTo;
}

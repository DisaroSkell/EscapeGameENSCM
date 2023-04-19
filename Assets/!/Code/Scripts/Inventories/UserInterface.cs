using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
public abstract class UserInterface : MonoBehaviour
{
    public InventoryInterface inventory;

    public Player player;
    #nullable enable
    public Dictionary<InventorySlot, GameObject?> itemsDisplayed = new Dictionary<InventorySlot, GameObject?>();
    public Dictionary<GameObject, InventorySlot> slotsOfItems = new Dictionary<GameObject, InventorySlot>();
    // Start is called before the first frame update
    
    /// <summary>
    /// This function instantiates an object based on the item in a specific inventory slot and adds
    /// event triggers to it.
    /// </summary>
    /// <param name="i">The index of the item in the inventory that needs to be instantiated.</param>
    /// <returns>
    /// If the `item` variable is null, then the method returns without doing anything else.
    /// </returns>
    protected abstract void InstantiateObject(int i);
    
    void Start()
    {
        CreateDisplay();
    }
    // Update is called once per frame
    void Update()
    {
        UpdateDisplay();
    }

    public abstract void UpdateDisplay(bool update=false);
    public abstract void CreateDisplay();



    /// <summary>
    /// This function clears the inventory, items displayed, and slots of items when the application is
    /// quit and logs a message.
    /// </summary>
    private void OnApplicationQuit() {
        inventory.Clear();
        itemsDisplayed.Clear();
        slotsOfItems.Clear();
    }

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

    protected abstract void OnEnter(GameObject obj);
    
    /// <summary>
    /// The function clears the hover slot and object of the player's mouse item when an object exits.
    /// </summary>
    /// <param name="GameObject">The GameObject</param>
    protected void OnExit(GameObject obj) {
        player.mouseItem.hoverSlot = null;
        player.mouseItem.hoverObj = null;
    }
    /// <summary>
    /// The function creates a new game object with a raw image component and sets its parent and size
    /// based on the input object.
    /// </summary>
    /// <param name="GameObject">The object that is being dragged.</param>
    protected void OnDragStart(GameObject obj) {
        if(obj is null) {
            return;
        }
        if(!slotsOfItems.ContainsKey(obj)) {
            return;
        }
        ItemObject? item = slotsOfItems[obj].item;
        if(item is null) {
            return;
        }
        var mouseObject = new GameObject();
        mouseObject.transform.SetParent(obj.transform);
        var rt = mouseObject.AddComponent<RectTransform>();
        mouseObject.transform.SetParent(obj.transform.parent, false);
        var img = mouseObject.AddComponent<RawImage>();
        img.texture = item.prefab.GetComponent<RawImage>().texture;
        img.raycastTarget = false;
        RectTransform rtObj = item.prefab.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2((float)(rtObj.rect.width*0.8), (float)(rtObj.rect.height*0.8));
        rt.localScale = new Vector3(1, 1, 1);

        player.mouseItem.obj = mouseObject;
        player.mouseItem.itemSlot = slotsOfItems[obj];
    }
    /// <summary>
    /// The function removes an item from the inventory and destroys its corresponding game object.
    /// </summary>
    /// <param name="GameObject">GameObject is a class in Unity that represents a game object in the
    /// scene. In this context, it refers to the game object that was being dragged and dropped onto a
    /// slot in the inventory.</param>
    /// <returns>
    /// If the `item` variable is `null`, then the method will return and nothing will be done.
    /// </returns>
    protected abstract void OnDragEnd(GameObject obj);

    /// <summary>
    /// This function moves the position of a game object to the current mouse position during a drag
    /// event.
    /// </summary>
    /// <param name="GameObject">The GameObject parameter</param>
    /// <returns>
    /// If the `obj` parameter is null, the method will return without doing anything.
    /// </returns>
    protected void OnDrag(GameObject obj) {
        if(obj is null) {return;}
        if(player.mouseItem.obj is not null) {
            player.mouseItem.obj.GetComponent<RectTransform>().position = Input.mousePosition;
        }
    }
}
#nullable disable
public class MouseItem {
    public GameObject obj;
    public InventorySlot itemSlot;
    public InventorySlot hoverSlot;
    public GameObject hoverObj;
}

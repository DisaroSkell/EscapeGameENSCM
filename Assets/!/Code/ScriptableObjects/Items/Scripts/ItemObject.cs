using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {
    All,
    Document,
    Pictogram,
    Default
}
public abstract class ItemObject : ScriptableObject {
    public GameObject prefab;
    public ItemType type;
    public AbstractInventory inventory;
}

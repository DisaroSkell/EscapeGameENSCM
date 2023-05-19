using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {
    All,
    Document,
    Pictogram,
    Default,
    MatchesCard,
    Container,
    Triggered,
    Rotating
}
public abstract class ItemObject : ScriptableObject {
    public GameObject prefab;
    public ItemType type;
    public AbstractInventory inventory;
    
    # nullable enable
    public Interaction? interaction;

    public virtual GameObject GetPrefab() {
        return this.prefab;
    }

    #nullable enable
    // override object.Equals
    public virtual bool Equals(ItemObject? itemObj)
    {
        if(itemObj is null) return false;
        return this.prefab == itemObj.prefab;
    }
    
}

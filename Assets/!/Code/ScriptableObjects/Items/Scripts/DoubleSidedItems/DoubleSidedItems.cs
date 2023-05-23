using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DoubleSidedItems : ItemObject
{
    public GameObject backPrefab;

    public bool fliped;

    public bool canBeFlippedOnClick;

    public void Flip() {
        fliped = !fliped;
    }

    public override GameObject GetPrefab() {
        if(fliped) return this.backPrefab;
        else return this.prefab;
    }
}

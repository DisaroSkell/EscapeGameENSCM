using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName= "New Rotating Object", menuName = "Inventory System/Items/Rotating")]
public class RotatingItems : ItemObject
{
    public float currentAngle;

    public float degreesOfRotation;
    public void Rotate() {
        this.currentAngle += degreesOfRotation;
        this.currentAngle = this.currentAngle%360;
    }

    public override GameObject GetPrefab() {
        return this.prefab;
    }

    public void Awake() {
        type = ItemType.Rotating;
    }

    public float GetAngle() {
        return this.currentAngle;
    }

    public override bool Equals(ItemObject itemObj)
    {
        if(itemObj is null || itemObj.GetType() != typeof(RotatingItems)) return false;
        RotatingItems rObj = (RotatingItems)itemObj;
        return base.Equals(itemObj) && this.currentAngle == rObj.currentAngle;
    }

}
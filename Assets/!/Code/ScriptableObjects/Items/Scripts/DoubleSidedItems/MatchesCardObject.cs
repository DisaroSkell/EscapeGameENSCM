using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName= "New MatchesCard Object", menuName = "Inventory System/Items/MatchesCard")]
public class MatchesCardObject : DoubleSidedItems {
    public void Awake() {
        type = ItemType.MatchesCard;
   }
}

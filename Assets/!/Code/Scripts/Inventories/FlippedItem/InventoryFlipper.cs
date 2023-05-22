using System.Collections;
using System.Collections.Generic;
using UnityEngine;

# nullable enable
public class InventoryFlipper : MonoBehaviour
{
    public Inventory? inv;

    public void FlipAllItem() {
        if(inv is null) return;
        for (int i = 0; i < inv.Length; i++)
        {
            ItemObject? item = inv.GetItem(i);
            if(item is null) continue;
            if(item.GetType() == typeof(MatchesCardObject)) {
                MatchesCardObject it = (MatchesCardObject)item;
                it.Flip();
            }
        }
    }
}

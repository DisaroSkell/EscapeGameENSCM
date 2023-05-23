using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

# nullable enable
public class InventoryFlipper : MonoBehaviour
{
    public Inventory? inv;

    public TextMeshProUGUI? button_text;

    public bool power;

    private void Start() {
        if(button_text is null) return;
        if(power) button_text.text = "ON";
        else button_text.text = "OFF";
    }

    public void FlipAllItem() {
        if(inv is null) return;
        if(button_text is null) return;
        for (int i = 0; i < inv.Length; i++)
        {
            ItemObject? item = inv.GetItem(i);
            if(item is null) continue;
            if(item.GetType() == typeof(MatchesCardObject)) {
                MatchesCardObject it = (MatchesCardObject)item;
                it.Flip();
            }
        }
        power = !power;
        if(power) button_text.text = "ON";
        else button_text.text = "OFF";
    }
}

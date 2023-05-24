using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

# nullable enable
public class InventoryFlipper : MonoBehaviour
{
    public Inventory? inv;

    public TextMeshProUGUI? button_text;

    public AbstractInventoryDisplay? display;

    public bool power;

    private bool hasChanged = true;

    private void Start() {
        if(button_text is null) return;
        if(power) button_text.text = "OFF";
        else button_text.text = "ON";
    }

    private void Update() {
        SetAllCardState();
        if(display is not null) {
            if(hasChanged) {
                display.UpdateDisplay();
                hasChanged = false;
            }
        }
        
    }

    public void FlipAllItem() {
        if(button_text is null) return;
        power = !power;
        if(power) button_text.text = "OFF";
        else button_text.text = "ON";
    }

    public void SetAllCardState() {
        if(inv is null) return;
        for (int i = 0; i < inv.Length; i++)
        {
            ItemObject? item = inv.GetItem(i);
            if(item is null) continue;
            if(item.GetType() == typeof(MatchesCardObject)) {
                MatchesCardObject it = (MatchesCardObject)item;
                if(it.fliped != power) {
                    it.fliped = power;
                    hasChanged = true;
                }
            }
        }
    }
}

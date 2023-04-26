using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName= "New Document Object", menuName = "Inventory System/Items/Document")]

public class DocumentObject : ItemObject
{
    public Sprite oneImageSpriteOfThePDF;
    public void Awake() {
        type = ItemType.Document;
    }
}
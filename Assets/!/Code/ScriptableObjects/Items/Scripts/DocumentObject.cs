using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName= "New Document Object", menuName = "Inventory System/Items/Document")]
// a DocumentObject is an object that open a Document in the DocumentViewer when clicked in an inventory
public class DocumentObject : ItemObject {

    public string folderRessourcesName; // name of the folder of images in Assets/Ressources
    public void Awake() {
        type = ItemType.Document;
    }
}
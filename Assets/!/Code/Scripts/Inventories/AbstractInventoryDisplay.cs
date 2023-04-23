using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractInventoryDisplay : MonoBehaviour
{

    public AbstractInventory inventory;
    public Player player;

    public GameObject panel;

    public List<GameObject> objectList;

    # nullable enable

    // Start is called before the first frame update
    void Start() {
        Debug.Log("start");
        CreateDisplay();
    }
    public abstract void Display();

    public abstract void CreateDisplay();

    public virtual void UpdateDisplay() {
        this.ClearDisplay();
        this.Display();
    }

    public void ClearDisplay() {
        this.objectList.Clear();
        for (int i = 0; i < panel.transform.childCount; i++)
        {
            // Delete each child of the panel
            Destroy(panel.transform.GetChild(i).gameObject);
        }
    }
}

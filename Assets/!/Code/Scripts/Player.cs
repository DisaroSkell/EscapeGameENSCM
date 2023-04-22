using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public MouseItem mouseItem = new MouseItem();
    public PlayerMouse playerMouse = new PlayerMouse();
    
    void Start() {
        Debug.Log("START");
    }
}

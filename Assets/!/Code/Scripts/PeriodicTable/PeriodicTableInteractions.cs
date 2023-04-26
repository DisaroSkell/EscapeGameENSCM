using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeriodicTableInteractions : MonoBehaviour {
    public int nbElements;

    private MagnetInteraction magnet;
    private TableElement[] table;

    public void Initialize() {
        table = new TableElement[nbElements];

        for (int i = 0; i < nbElements; i++) {
            table[i].Initialize(/*params*/);
        }
    }

    public void SetMagnet(MagnetInteraction magnet) {
        this.magnet = magnet;
    }

    public void MoveMagnet(Vector3 coords) {
        if (magnet is not null) {
            magnet.transform.position = coords;
        }
    }
}

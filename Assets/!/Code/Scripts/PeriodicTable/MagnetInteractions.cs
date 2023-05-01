using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetInteraction : MonoBehaviour {
    public bool hasKey;

    private (int, int) indexPosition;

    public (int, int) GetCellPosition() {
        return this.indexPosition;
    }

    public void horizontalMouvement(int newColumn) {
        this.indexPosition = (this.indexPosition.Item1, newColumn);
    }

    public void verticalMouvement(int newLine) {
        this.indexPosition = (newLine, this.indexPosition.Item2);
    }
}

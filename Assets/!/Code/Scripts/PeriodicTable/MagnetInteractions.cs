using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetInteractions : MonoBehaviour {
    public bool hasKey;

    private (int, int) indexPosition = (0, 0);

    public (int, int) GetCellPosition() {
        return this.indexPosition;
    }

    public void horizontalMouvement(int newColumn) {
        PeriodicTableMaze table = this.GetComponentInParent<PeriodicTableMaze>();

        AtomCell previousCell = table.GetMaze().maze[indexPosition.Item1][indexPosition.Item2];
        AtomCell goalCell = table.GetMaze().maze[indexPosition.Item1][newColumn];

        this.transform.localPosition += goalCell.position - previousCell.position;

        this.indexPosition = (this.indexPosition.Item1, newColumn);
    }

    public void verticalMouvement(int newLine) {
        PeriodicTableMaze table = this.GetComponentInParent<PeriodicTableMaze>();

        AtomCell previousCell = table.GetMaze().maze[indexPosition.Item1][indexPosition.Item2];
        AtomCell goalCell = table.GetMaze().maze[newLine][indexPosition.Item2];

        this.transform.localPosition += goalCell.position - previousCell.position;

        this.indexPosition = (newLine, this.indexPosition.Item2);
    }
}

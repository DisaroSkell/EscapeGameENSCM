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

        if (this.hasKey) {
            makeKeyFollow(table);
        }
    }

    public void verticalMouvement(int newLine) {
        PeriodicTableMaze table = this.GetComponentInParent<PeriodicTableMaze>();

        AtomCell previousCell = table.GetMaze().maze[indexPosition.Item1][indexPosition.Item2];
        AtomCell goalCell = table.GetMaze().maze[newLine][indexPosition.Item2];

        this.transform.localPosition += goalCell.position - previousCell.position;

        this.indexPosition = (newLine, this.indexPosition.Item2);

        if (this.hasKey) {
            makeKeyFollow(table);
        }
    }

    public void makeKeyFollow(PeriodicTableMaze table) {
        KeyInteractions key = table.key;

        if (key is not null) {
            key.transform.localPosition = new Vector3(this.transform.localPosition.x,
                                                      this.transform.localPosition.y,
                                                      key.transform.localPosition.z);
        } else {
            this.hasKey = false;
        }
    }
}

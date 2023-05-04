using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetInteractions : MonoBehaviour {
    public bool hasKey;

    // Position of the magnet in the maze.
    //(-1, -1) corresponds to the bottom of the maze.
    //(-2, -2) corresponds to the top of the maze.
    private (int, int) indexPosition = (0, 0);

    public (int, int) GetCellPosition() {
        return this.indexPosition;
    }

    /// <summary>
    /// Moves the magnet horizontally to the given column.
    /// </summary>
    /// <param name="newColumn">Column to move to.</param>
    public void HorizontalMouvement(int newColumn) {
        this.Mouvement((this.indexPosition.Item1, newColumn));
    }

    /// <summary>
    /// Moves the magnet vertically to the given line.
    /// </summary>
    /// <param name="newLine">Line to move to.</param>
    public void VerticalMouvement(int newLine) {
        this.Mouvement((newLine, this.indexPosition.Item2));
    }

    /// <summary>
    /// Moves the magnet to the given position.
    /// </summary>
    /// <param name="goalPosition">Indexes of the cell to move to.</param>
    private void Mouvement((int, int) goalPosition) {
        PeriodicTableMaze table = this.GetComponentInParent<PeriodicTableMaze>();

        AtomCell previousCell = table.GetMaze().maze[indexPosition.Item1][indexPosition.Item2];
        AtomCell goalCell = table.GetMaze().maze[goalPosition.Item1][goalPosition.Item2];

        if (this.hasKey) {
            this.EncounterWallCheck(table, this.indexPosition, goalPosition);
        }

        this.transform.localPosition += goalCell.position - previousCell.position;

        this.indexPosition = goalPosition;

        if (this.hasKey) {
            MakeKeyFollow(table);
        }
    }

    /// <summary>
    /// If the key exists and the magnet has it, makes the key follow the magnet in its mouvements.
    /// </summary>
    /// <param name="table">The periodic table the magnet and the key are linked to.</param>
    public void MakeKeyFollow(PeriodicTableMaze table) {
        KeyInteractions key = table.key;

        if (key is not null) {
            key.transform.localPosition = new Vector3(this.transform.localPosition.x,
                                                      this.transform.localPosition.y,
                                                      key.transform.localPosition.z);
        } else {
            this.hasKey = false;
        }
    }

    /// <summary>
    /// Check if the magnet has encountered a wall on its way.
    /// If it did, it makes the key fall and sets the hasKey attribute to false.
    /// It is better to call this method only if the magnet has the key.
    /// </summary>
    /// <param name="table">The periodic table the magnet is linked to.</param>
    /// <param name="prevCell">The cell the magnet is coming from.</param>
    /// <param name="goalCell">The cell the magnet is going to.</param>
    public void EncounterWallCheck(PeriodicTableMaze table, (int, int) prevCell, (int, int) goalCell) {
        if (prevCell == (-1, -1)) {
            return;
        }

        (int, int)[] indexSet = {prevCell, goalCell};
        Direction[] directions = {Direction.None, Direction.None};

        bool encounteredWall = table.GetMaze().EncountersWallOnPath(new MazePath(indexSet, directions));

        if (encounteredWall) {
            this.hasKey = false;
            table.MakeKeyFall();
        }
    }
}

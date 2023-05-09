using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetInteractions : MonoBehaviour {
    public bool hasKey;

    public ItemObject keyObject;

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
    /// Moves the magnet to the top of the table.
    /// It moves it in a straight line to the top.
    /// If it doesn't encounter a wall and it has the key, the player gets the key in its inventory.
    /// </summary>
    public void ToTopMouvement() {
        this.Mouvement((-2, -2));

        // If the magnet has the key while being at the top, the maze is solved.
        if (this.hasKey) {
            PeriodicTableMaze table = this.GetComponentInParent<PeriodicTableMaze>();

            // adding key to the player inventory
            if(this.keyObject is not null) {
                keyObject.inventory.AddItem(keyObject);
                this.keyObject = null;
            }
            
            // Destroy key object
            if(table.key is not null) {
                Destroy(table.key.gameObject);
                table.key = null;
                this.hasKey = false;
            }
        }
    }

    /// <summary>
    /// Moves the magnet to the bottom of the table.
    /// It moves straight to the default key position and collects it.
    /// </summary>
    public void ToBottomMouvement() {
        this.Mouvement((-1, -1));
    }

    /// <summary>
    /// Moves the magnet from the top of the table to the top row at the given column.
    /// </summary>
    /// <param name="newColumn">Column to move to.</param>
    public void FromTopMouvement(int newColumn) {
        this.Mouvement((0, newColumn));
    }

    /// <summary>
    /// Moves the magnet from the bottm of the table to the bottom row at the given column.
    /// </summary>
    /// <param name="newColumn">Column to move to.</param>
    public void FromBottomMouvement(int newColumn) {
        PeriodicTableMaze table = this.GetComponentInParent<PeriodicTableMaze>();

        this.Mouvement((table.GetMaze().GetVSize()-1, newColumn));

        // Enter the maze => check if it's a gate
        if (!table.GetMaze().GetCellAt(table.GetMaze().GetVSize()-1, newColumn).IsOpened(Direction.South)) {
            this.hasKey = false;
            table.MakeKeyFall();
        }
    }

    /// <summary>
    /// Moves the magnet to the given position.
    /// (-1, -1) corresponds to the bottom of the maze.
    /// (-2, -2) corresponds to the top of the maze.
    /// </summary>
    /// <param name="goalPosition">Indexes of the cell to move to.</param>
    private void Mouvement((int, int) goalPosition) {
        PeriodicTableMaze table = this.GetComponentInParent<PeriodicTableMaze>();

        // We first determine the previous position
        Vector3 previousCellPos;

        if (this.indexPosition == (-1, -1)) {
            previousCellPos = table.GetBottomPosition();
        } else if (this.indexPosition == (-2, -2)) {
            previousCellPos = table.GetTopPosition();
        } else {
            previousCellPos = table.GetMaze().maze[this.indexPosition.Item1][this.indexPosition.Item2].position;
        }

        // We then determine the goal position
        Vector3 goalCellPos;

        if (goalPosition == (-1, -1)) {
            goalCellPos = table.GetBottomPosition();
        } else if (goalPosition == (-2, -2)) {
            goalCellPos = table.GetTopPosition();

            if (this.hasKey) {
                // We check wall encounters until the exit
                this.EncounterWallCheck(table, this.indexPosition, (0, this.indexPosition.Item2));

                // Then we check the maze exit
                if (!table.GetMaze().GetCellAt(0, this.indexPosition.Item2).IsOpened(Direction.North)) {
                    this.hasKey = false;
                    table.MakeKeyFall();
                }
            }
        } else {
            goalCellPos = table.GetMaze().maze[goalPosition.Item1][goalPosition.Item2].position;

            if (this.hasKey) {
                this.EncounterWallCheck(table, this.indexPosition, goalPosition);
            }
        }

        this.transform.localPosition += goalCellPos - previousCellPos;

        this.indexPosition = goalPosition;

        if (this.hasKey) {
            MakeKeyFollow(table);
        } else if (goalPosition == (-1, -1) && table.key is not null) {
            this.hasKey = true;
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
    /// Precondition: Do not use this if entering or exiting the maze.
    /// </summary>
    /// <param name="table">The periodic table the magnet is linked to.</param>
    /// <param name="prevCell">The cell the magnet is coming from.</param>
    /// <param name="goalCell">The cell the magnet is going to.</param>
    public void EncounterWallCheck(PeriodicTableMaze table, (int, int) prevCell, (int, int) goalCell) {
        if (prevCell == (-1, -1) || prevCell == (-2, -2) || goalCell == (-1, -1) || goalCell == (-2, -2) || prevCell == goalCell) {
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

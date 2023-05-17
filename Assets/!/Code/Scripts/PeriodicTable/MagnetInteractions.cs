using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetInteractions : MonoBehaviour {
    public bool hasKey;

    public ItemObject keyObject;

    // Periodic table linked to the magnet
    private PeriodicTableMaze table;

    // Position of the magnet in the maze.
    //(-1, -1) corresponds to the bottom of the maze.
    //(-2, -2) corresponds to the top of the maze.
    private (int, int) indexPosition = (0, 0);

    public void Initialize(ItemObject key) {
        this.keyObject = key;
        this.table = this.GetComponentInParent<PeriodicTableMaze>();
    }

    public (int, int) GetCellPosition() {
        return this.indexPosition;
    }

    /// <summary>
    /// Moves the magnet horizontally to the given column.
    /// </summary>
    /// <param name="newColumn">Column to move to.</param>
    public void HorizontalMouvement(int newColumn) {
        if (this.hasKey) {
            // Turn off highlighting for previous column except the cell it was on.
            this.table.GlowOffColumn(this.indexPosition.Item2);
            this.table.GlowOnCell(this.indexPosition.Item1, this.indexPosition.Item2);
        }

        this.Mouvement((this.indexPosition.Item1, newColumn));

        if (this.hasKey) {
            // Turn one highlighting for next column.
            this.table.GlowOnColumn(newColumn);
        }
    }

    /// <summary>
    /// Moves the magnet vertically to the given line.
    /// </summary>
    /// <param name="newLine">Line to move to.</param>
    public void VerticalMouvement(int newLine) {
        if (this.hasKey) {
            // Turn off highlighting for previous line except the cell it was on.
            this.table.GlowOffLine(this.indexPosition.Item1);
            this.table.GlowOnCell(this.indexPosition.Item1, this.indexPosition.Item2);
        }

        this.Mouvement((newLine, this.indexPosition.Item2));

        if (this.hasKey) {
            // Turn one highlighting for next line.
            this.table.GlowOnLine(newLine);
        }
    }

    /// <summary>
    /// Moves the magnet to the top of the table.
    /// It moves it in a straight line to the top.
    /// If it doesn't encounter a wall and it has the key, the player gets the key in its inventory.
    /// </summary>
    public void ToTopMouvement() {
        if (this.hasKey) {
            // Turn off highlighting for previous line and column of the cell it was on.
            this.table.GlowOffLine(this.indexPosition.Item1);
            this.table.GlowOffColumn(this.indexPosition.Item2);
        }

        this.Mouvement((-2, -2));

        // If the magnet has the key while being at the top, the maze is solved.
        if (this.hasKey) {
            // adding key to the player inventory
            if(this.keyObject is not null) {
                keyObject.inventory.AddItem(keyObject);
                this.keyObject = null;
            }
            
            // Destroy key object
            if(this.table.key is not null) {
                Destroy(this.table.key.gameObject);
                this.table.key = null;
                this.hasKey = false;
            }
        }
    }

    /// <summary>
    /// Moves the magnet to the bottom of the table.
    /// It moves straight to the default key position and collects it.
    /// </summary>
    public void ToBottomMouvement() {
        // We authorize top to bottom and bottom to bottom mouvements, so we have to be carefull.
        if (this.indexPosition != (-1, -1) && this.indexPosition != (-2, -2) && this.hasKey) {
            // Turn off highlighting for previous line and column of the cell it was on.
            this.table.GlowOffLine(this.indexPosition.Item1);
            this.table.GlowOffColumn(this.indexPosition.Item2);
        }

        this.Mouvement((-1, -1));
    }

    /// <summary>
    /// Moves the magnet from the top of the table to the top row at the given column.
    /// </summary>
    /// <param name="newLine">Line to move to.</param>
    /// <param name="newColumn">Column to move to.</param>
    public void FromTopMouvement(int newLine, int newColumn) {
        // First we enter the maze
        this.Mouvement((0, newColumn));

        // Then we go where we wanted
        this.Mouvement((newLine, newColumn));

        if (this.hasKey) {
            // Turn on highlighting for the line and column it goes to.
            this.table.GlowOnLine(newLine);
            this.table.GlowOnColumn(newColumn);
        }
    }

    /// <summary>
    /// Moves the magnet from the bottm of the table to the bottom row at the given column.
    /// </summary>
    /// <param name="newLine">Line to move to.</param>
    /// <param name="newColumn">Column to move to.</param>
    public void FromBottomMouvement(int newLine, int newColumn) {
        // First we enter the maze
        this.Mouvement((this.table.GetMaze().GetVSize()-1, newColumn));

        // Enter the maze => check if it's a gate
        if (!this.table.GetMaze().GetCellAt(this.table.GetMaze().GetVSize()-1, newColumn).IsOpened(Direction.South)) {
            this.hasKey = false;
            this.table.MakeKeyFall();
        }

        // Then we go where we wanted
        this.Mouvement((newLine, newColumn));

        if (this.hasKey) {
            // Turn on highlighting for the line and column it goes to.
            this.table.GlowOnLine(newLine);
            this.table.GlowOnColumn(newColumn);
        }
    }

    /// <summary>
    /// Moves the magnet to the given position.
    /// (-1, -1) corresponds to the bottom of the maze.
    /// (-2, -2) corresponds to the top of the maze.
    /// </summary>
    /// <param name="goalPosition">Indexes of the cell to move to.</param>
    private void Mouvement((int, int) goalPosition) {
        // We first determine the previous position
        Vector3 previousCellPos;

        if (this.indexPosition == (-1, -1)) {
            previousCellPos = this.table.GetBottomPosition();
        } else if (this.indexPosition == (-2, -2)) {
            previousCellPos = this.table.GetTopPosition();
        } else {
            previousCellPos = this.table.GetMaze().maze[this.indexPosition.Item1][this.indexPosition.Item2].position;
        }

        // We then determine the goal position
        Vector3 goalCellPos;

        if (goalPosition == (-1, -1)) {
            goalCellPos = this.table.GetBottomPosition();
        } else if (goalPosition == (-2, -2)) {
            goalCellPos = this.table.GetTopPosition();

            if (this.hasKey) {
                // We check wall encounters until the exit
                this.EncounterWallCheck(this.indexPosition, (0, this.indexPosition.Item2));

                // Then we check the maze exit
                if (!this.table.GetMaze().GetCellAt(0, this.indexPosition.Item2).IsOpened(Direction.North)) {
                    this.hasKey = false;
                    this.table.MakeKeyFall();
                }
            }
        } else {
            goalCellPos = this.table.GetMaze().maze[goalPosition.Item1][goalPosition.Item2].position;

            if (this.hasKey) {
                this.EncounterWallCheck(this.indexPosition, goalPosition);
            }
        }

        this.transform.localPosition += goalCellPos - previousCellPos;

        this.indexPosition = goalPosition;

        if (this.hasKey) {
            MakeKeyFollow();
        } else if (goalPosition == (-1, -1) && this.table.key is not null) {
            this.hasKey = true;
        }
    }

    /// <summary>
    /// If the key exists and the magnet has it, makes the key follow the magnet in its mouvements.
    /// </summary>
    public void MakeKeyFollow() {
        KeyInteractions key = this.table.key;

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
    /// <param name="prevCell">The cell the magnet is coming from.</param>
    /// <param name="goalCell">The cell the magnet is going to.</param>
    public void EncounterWallCheck((int, int) prevCell, (int, int) goalCell) {
        if (prevCell == (-1, -1) || prevCell == (-2, -2) || goalCell == (-1, -1) || goalCell == (-2, -2) || prevCell == goalCell) {
            return;
        }

        (int, int)[] indexSet = {prevCell, goalCell};
        Direction[] directions = {Direction.None, Direction.None};

        bool encounteredWall = this.table.GetMaze().EncountersWallOnPath(new MazePath(indexSet, directions));

        if (encounteredWall) {
            this.hasKey = false;
            this.table.MakeKeyFall();
            this.table.GlowOffLine(prevCell.Item1);
            this.table.GlowOffColumn(prevCell.Item2);
        }
    }
}

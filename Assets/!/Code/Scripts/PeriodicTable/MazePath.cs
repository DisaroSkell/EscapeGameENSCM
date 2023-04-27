using System.Collections.Generic;
using System.Linq;

public class MazePath {
    private List<(int,int)> indexSet;

    private List<Direction> directions;

    // TODO : Refactor this
    public MazePath(Maze maze, (int, int)[] indexSet, Direction[] directions) {
        if (indexSet.Length != indexSet.Distinct().Count()) {
            // STOP HERE: Can't create path from duplicates cells
        }

        if (indexSet.Length != directions.Length) {
            // STOP HERE: Must have a direction (even if it's Direction.None) for every cell
        }

        this.indexSet = new List<(int, int)>();
        this.directions = new List<Direction>();

        // We create a way between every cell
        for (int i = 0; i < indexSet.Length-1; i++) {
            int line = indexSet[i].Item1;
            int column = indexSet[i].Item2;
            Direction direction = directions[i];

            // We first go in the direction given and add all cells in the way until aligned
            switch (direction) {
                case Direction.North :
                    // Go North
                    if (line >= indexSet[i+1].Item1){
                        while (line != indexSet[i+1].Item1) {
                            this.indexSet.Add((line, column));
                            this.directions.Add(direction);
                            line--;
                        }
                    } else {
                        // STOP HERE: We are going opposite direction => can't create path
                    }
                    break;
                case Direction.South :
                    // Go South
                    if (line <= indexSet[i+1].Item1){
                        while (line != indexSet[i+1].Item1) {
                            this.indexSet.Add((line, column));
                            this.directions.Add(direction);
                            line++;
                        }
                    } else {
                        // STOP HERE: We are going opposite direction => can't create path
                    }
                    break;
                case Direction.East :
                    // Go East
                    if (column <= indexSet[i+1].Item2){
                        while (column != indexSet[i+1].Item2) {
                            this.indexSet.Add((line, column));
                            this.directions.Add(direction);
                            column++;
                        }
                    } else {
                        // STOP HERE: We are going opposite direction => can't create path
                    }
                    break;
                case Direction.West :
                    // Go West
                    if (column >= indexSet[i+1].Item2){
                        while (column != indexSet[i+1].Item2) {
                            this.indexSet.Add((line, column));
                            this.directions.Add(direction);
                            column--;
                        }
                    } else {
                        // STOP HERE: We are going opposite direction => can't create path
                    }
                    break;
            }

            // First detect what direction to take
            // Then move to the cell and take all cells on the way

            if (line == indexSet[i+1].Item1) {
                if (column > indexSet[i+1].Item2) {
                    // Go West
                    direction = Direction.West;
                    while (column != indexSet[i+1].Item2) {
                        this.indexSet.Add((line, column));
                        this.directions.Add(direction);
                        column--;
                    }
                } else {
                    // Go East
                    direction = Direction.East;
                    while (column != indexSet[i+1].Item2) {
                        this.indexSet.Add((line, column));
                        this.directions.Add(direction);
                        column++;
                    }
                }
            } else if (column == indexSet[i+1].Item2) {
                if (line > indexSet [i+1].Item1) {
                    // Go North
                    direction = Direction.North;
                    while (line != indexSet[i+1].Item1) {
                        this.indexSet.Add((line, column));
                        this.directions.Add(direction);
                        line--;
                    }
                } else {
                    // Go South
                    direction = Direction.South;
                    while (line != indexSet[i+1].Item1) {
                        this.indexSet.Add((line, column));
                        this.directions.Add(direction);
                        line++;
                    }
                }
            } else {
                // STOP HERE: Not aligned with next cell => Missing direction
            }
        }
        // We add last cell
        this.indexSet.Add(indexSet[indexSet.Length-1]);

        if (this.indexSet.Count != this.indexSet.Distinct().Count()) {
            // STOP HERE: We added the same cell twice => not a valid way
        }
    }

    public List<(int, int)> getIndexSet() {
        return this.indexSet;
    }

    public List<Direction> getDirections() {
        return this.directions;
    }
}
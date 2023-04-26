using System.Collections.Generic;
using System.Linq;

public class MazePath {
    private List<(int,int)> indexSet;

    private List<Direction> directions;

    public MazePath(Maze maze, (int, int)[] indexSet, Direction[] directions) {
        if (indexSet.Length != indexSet.Distinct().Count()) {
            // STOP HERE
        }

        if (indexSet.Length != directions.Length) {
            // STOP HERE
        }

        this.indexSet = new List<(int, int)>();
        this.directions = new List<Direction>();

        for (int i = 0; i < indexSet.Length-1; i++) {
            int line = indexSet[i].Item1;
            int column = indexSet[i].Item2;
            Direction direction = directions[i];

            switch (direction) {
                case Direction.North :
                    if (line > 0 && line >= indexSet[i+1].Item1){
                        while (line != indexSet[i+1].Item1) {
                            this.indexSet.Add((line, column));
                            this.directions.Add(direction);
                            line--;
                        }
                    } else {
                        // STOP HERE
                    }
                    break;
                case Direction.South :
                    if (line < maze.GetHSize() - 1 && line <= indexSet[i+1].Item1){
                        while (line != indexSet[i+1].Item1) {
                            this.indexSet.Add((line, column));
                            this.directions.Add(direction);
                            line++;
                        }
                    } else {
                        // STOP HERE
                    }
                    break;
                case Direction.East :
                    if (column < maze.GetVSize() - 1 && column <= indexSet[i+1].Item2){
                        while (column != indexSet[i+1].Item2) {
                            this.indexSet.Add((line, column));
                            this.directions.Add(direction);
                            column++;
                        }
                    } else {
                        // STOP HERE
                    }
                    break;
                case Direction.West :
                    if (column > 0 && column >= indexSet[i+1].Item2){
                        while (column != indexSet[i+1].Item2) {
                            this.indexSet.Add((line, column));
                            this.directions.Add(direction);
                            column--;
                        }
                    } else {
                        // STOP HERE
                    }
                    break;
            }
            // TODO Go on next cell if aligned

            // First detect what direction to take
            // Then move to the cell

            if (line == indexSet[i+1].Item1) {
                // Aligned horizontally
            } else if (column == indexSet[i+1].Item2) {
                // Aligned vertically
            } else {
                // STOP HERE
            }
        }
        // TODO verify that we add all the cells on the way

        if (this.indexSet.Count != this.indexSet.Distinct().Count()) {
            // STOP HERE
        }
    }

    public List<(int, int)> getIndexSet() {
        return this.indexSet;
    }

    public List<Direction> getDirections() {
        return this.directions;
    }
}
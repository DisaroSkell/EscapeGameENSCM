using System;
using System.Collections.Generic;
using System.Linq;

public class MazePath {
    // List of the cell composing the path.
    private List<(int,int)> indexSet;

    // List of the direction to go from one cell to the next.
    private List<Direction> directions;

    /// <summary>
    /// Constructor of path.
    /// Creates a path going through all the cell given using the directions given.
    /// It tries to be as straight as possible.
    /// Preconditions:
    ///                - The path can't cross itself.
    ///                - Following the previous rule, indexSet can't contain the same cell twice.
    ///                - directions and indexSet must be same sized.
    ///                - If one cell and the next aren't aligned, the direction given must go in the next cell's way.
    /// </summary>
    /// <param name="indexSet">Cell to go though when creating the path.</param>
    /// <param name="directions">Directions to follow to create the path.</param>
    /// <returns>
    /// Returns the created path if the preconditions are fulfilled.
    /// Else raises an ArgumentException.
    /// </returns>
    public MazePath((int, int)[] indexSet, Direction[] directions) {
        if (indexSet.Length != indexSet.Distinct().Count()) {
            // STOP HERE: Can't create path from duplicates cells
            throw new ArgumentException("Can't have duplicates in indexSet !", "indexSet");
        }

        if (indexSet.Length != directions.Length) {
            // STOP HERE: Must have a direction (even if it's Direction.None) for every cell
            throw new ArgumentException("directions must be the same length as indexSet !", "directions");
        }

        this.indexSet = new List<(int, int)>();
        this.directions = new List<Direction>();

        // We create a way between every cell
        for (int i = 0; i < indexSet.Length-1; i++) {
            // We first go in the direction given and add all cells in the way until aligned
            (int line, int column) = this.AddCellsOnTheWay(indexSet[i], indexSet[i+1], directions[i]);

            // Then we detect what direction to take
            // And finally we move to the cell and take all cells on the way

            if (line == indexSet[i+1].Item1) {
                if (column > indexSet[i+1].Item2) {
                    // Go West
                    this.AddCellsOnTheWay((line, column), indexSet[i+1], Direction.West);
                } else {
                    // Go East
                    this.AddCellsOnTheWay((line, column), indexSet[i+1], Direction.East);
                }
            } else if (column == indexSet[i+1].Item2) {
                if (line > indexSet [i+1].Item1) {
                    // Go North
                    this.AddCellsOnTheWay((line, column), indexSet[i+1], Direction.North);
                } else {
                    // Go South
                    this.AddCellsOnTheWay((line, column), indexSet[i+1], Direction.South);
                }
            } else {
                // STOP HERE: Not aligned with next cell => Missing direction
                throw new ArgumentException("directions[i] must provide a direction to go from indexSet[i] to indexSet[i+1] if their indexes are not aligned !", "directions");
            }
        }
        
        // We add last cell
        this.indexSet.Add(indexSet[indexSet.Length-1]);
        this.directions.Add(Direction.None);

        if (this.indexSet.Count != this.indexSet.Distinct().Count()) {
            // STOP HERE: We added the same cell twice => not a valid way
            throw new ArgumentException("Can't create a path that does not cross himself with given parameters !");
        }
    }

    /// <summary>
    /// Add all cells found between start (included) and goal (excluded) to this.indexSet.
    /// Preconditions:
    ///                - If start cell and goal cell aren't aligned, the direction given must go in the goal cell's way.
    /// </summary>
    /// <param name="start">Coordinates corresponding to the starting cell.</param>
    /// <param name="goal">Coordinates corresponding to the goal cell.</param>
    /// <param name="direction">Direction to go to the goal cell.</param>
    /// <returns>
    /// Returns the final position if the preconditions are fulfilled.
    /// Else raises an ArgumentException.
    /// </returns>
    private (int, int) AddCellsOnTheWay((int, int) start, (int, int) goal, Direction direction) {
        int line = start.Item1;
        int column = start.Item2;
        string errMessage = "direction parameter must provide a correct direction to go from start to goal parameters !";
        
        switch (direction) {
            case Direction.North :
                // Go North
                if (line >= goal.Item1){
                    while (line != goal.Item1) {
                        this.indexSet.Add((line, column));
                        this.directions.Add(direction);
                        line--;
                    }
                } else {
                    // STOP HERE: We are going opposite direction => can't create path
                    throw new ArgumentException(errMessage, "directions");
                }
                break;
            case Direction.South :
                // Go South
                if (line <= goal.Item1){
                    while (line != goal.Item1) {
                        this.indexSet.Add((line, column));
                        this.directions.Add(direction);
                        line++;
                    }
                } else {
                    // STOP HERE: We are going opposite direction => can't create path
                    throw new ArgumentException(errMessage, "directions");
                }
                break;
            case Direction.East :
                // Go East
                if (column <= goal.Item2){
                    while (column != goal.Item2) {
                        this.indexSet.Add((line, column));
                        this.directions.Add(direction);
                        column++;
                    }
                } else {
                    // STOP HERE: We are going opposite direction => can't create path
                    throw new ArgumentException(errMessage, "directions");
                }
                break;
            case Direction.West :
                // Go West
                if (column >= goal.Item2){
                    while (column != goal.Item2) {
                        this.indexSet.Add((line, column));
                        this.directions.Add(direction);
                        column--;
                    }
                } else {
                    // STOP HERE: We are going opposite direction => can't create path
                    throw new ArgumentException(errMessage, "directions");
                }
                break;
        }

        return (line, column);
    }

    public List<(int, int)> GetIndexSet() {
        return this.indexSet;
    }

    public List<Direction> GetDirections() {
        return this.directions;
    }
}
using System;
using System.Collections.Generic;

public class PathBuilder {
    private int line;
    private int column;

    public PathBuilder(int l, int c){
        this.line = l;
        this.column = c;
    }

    public int GetLine(){
        return this.line;
    }

    public int GetColumn(){
        return this.column;
    }

    private void SetLine(int l){
        this.line = l;
    }

    private void SetColumn(int c){
        this.column = c;
    }

    public void CreatePaths(Maze maze) {
        while(!maze.IsMazeVisited()){
            Direction direction = ChooseNeighbourDirection(maze);
            int[] ncPos = maze.FindNeighbourCellWithDirection(this.GetColumn(), this.GetLine(), direction);
            
            if(!maze.IsCellVisited(ncPos[0], ncPos[1])){
                maze.OpenWall(this.GetColumn(), this.GetLine(), direction);
                maze.SetCellAsVisited(ncPos[0], ncPos[1]);
            }

            switch (direction) {
                case Direction.North :
                    this.SetColumn(this.GetColumn() - 1);
                    break;
                case Direction.South :
                    this.SetColumn(this.GetColumn() + 1);
                    break;
                case Direction.East :
                    this.SetLine(this.GetLine() + 1);
                    break;
                case Direction.West :
                    this.SetLine(this.GetLine() - 1);
                    break;
            }
        }
    }

    public void CreatePath(Maze maze, MazePath path) {
        List<(int, int)> indexSet = path.getIndexSet();
        List<Direction> directions = path.getDirections();

        // We open the 2 gates
        maze.OpenTwoGateAt(indexSet[0], indexSet[indexSet.Count-1]);
        
        // We go through the path.indexSet and path.directions to open the according walls in the maze
        List<Direction>.Enumerator directionsIt = directions.GetEnumerator();

        foreach (var coord in indexSet) {
            directionsIt.MoveNext();
            maze.OpenWall(coord.Item1, coord.Item2, directionsIt.Current);
        }
    }

    private Direction ChooseNeighbourDirection(Maze maze){
        Random r = new Random();
        bool[] possibleDirection = maze.Neighbour(this.GetColumn(), this.GetLine());
        bool possible = false;
        int n = 0;
        while(!possible){
            n = r.Next(1, 5);
            possible = possibleDirection[n];
        }

        switch(n){
            case 0 : return Direction.North;
            case 1 : return Direction.South;
            case 2 : return Direction.East;
            case 3 : return Direction.West;
        }

        return Direction.None;
    }
}
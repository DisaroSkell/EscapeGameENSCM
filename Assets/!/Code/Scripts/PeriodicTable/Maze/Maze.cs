using System;
using System.Collections.Generic;

public class Maze<Cell> where Cell : MazeCell, new() {
    private int hSize;

    private int vSize;

    private int nbGates;

    public Cell[][] maze;

    public Maze(int hSize, int vSize) {
        this.hSize = hSize;
        this.vSize = vSize;

        this.nbGates = 0;
        this.maze = new Cell[vSize][];

        for (int i = 0; i < vSize; i++) {
            this.maze[i] = new Cell[hSize];

            for (int j = 0; j < hSize; j++) {
                this.maze[i][j] = new Cell();
            }
        }

        Random rand = new Random();
    }

    public void Explore() {
        this.CreatePaths();
        this.OpenTwoGate();
    }

    public void Explore(MazePath path) {
        this.CreatePath(path);
        this.CreatePaths();
        this.OpenTwoGate();
    }

    public void OpenTwoGate() {
        if (this.nbGates != 0) {
            return;
        }

        this.OpenWall(0, 0, Direction.West);
        this.OpenWall(this.vSize - 1, this.hSize - 1, Direction.East);
        
        this.nbGates += 2;
    }

    public void OpenTwoGateAt((int, int) gate1, (int, int) gate2) {
        if (this.nbGates != 0) {
            return;
        }

        this.OpenGate(gate1);

        this.OpenGate(gate2);
        
        this.nbGates += 2;
    }

    private void OpenGate((int, int) gate) {
        if (this.nbGates >= 2) {
            return;
        }

        Direction direction = Direction.None;
        
        if (gate.Item1 == 0) direction = Direction.North;
        if (gate.Item1 == vSize-1) direction = Direction.South;
        if (gate.Item2 == 0) direction = Direction.West;
        if (gate.Item2 == hSize-1) direction = Direction.East;

        OpenWall(gate.Item1, gate.Item2, direction);
    }

    public int GetNbGates() {
        return this.nbGates;
    }

    public void OpenWall(int l, int c, Direction direction) {
        if (((l == 0 && direction == Direction.North) || (l == vSize-1 && direction == Direction.South) || (c == 0 && direction == Direction.West) || (c == hSize-1 && direction == Direction.East))) {
            if (this.GetNbGates() >= 2) {
                return;
            }
            this.nbGates += 1;
        }
        this.maze[l][c].OpenWall(direction);
        switch (direction) {
            case Direction.North :
                if (l > 0){
                    this.maze[l-1][c].OpenWall(Direction.South);
                }
                break;
            case Direction.South :
                if (l < this.vSize - 1){
                    this.maze[l+1][c].OpenWall(Direction.North);
                }
                break;
            case Direction.East :
                if (c < this.hSize - 1){
                    this.maze[l][c+1].OpenWall(Direction.West);
                }
                break;
            case Direction.West :
                if (c > 0){
                    this.maze[l][c-1].OpenWall(Direction.East);
                }
                break;
        }
    }

    public bool IsOpened(int l, int c, Direction direction) {
        return this.maze[l][c].IsOpened(direction);
    }

    public Cell GetCellAt(int l, int c) {
        return this.maze[l][c];
    }

    public bool[] Neighbour(int l, int c) {
        bool[] n = new bool[4];
        for (int i = 0; i < 4; i++) {
            n[i] = false;
        }
        if (l > 0) {
            n[0] = true;
        }
        if (l < this.vSize - 1) {
            n[1] = true;
        }
        if (c > 0) {
            n[3] = true;
        }
        if (c < this.hSize - 1) {
            n[2] = true;
        }
        return n;
    }

    public int[] FindNeighbourCellWithDirection(int l, int c, Direction direction) {
        int[] tmp = {l, c};

        switch (direction) {
            case Direction.North :
                tmp[0] -= 1;
                break;
            case Direction.South :
                tmp[0] += 1;
                break;
            case Direction.East :
                tmp[1] += 1;
                break;
            case Direction.West :
                tmp[1] -= 1;
                break;
        }

        return tmp;
    }

    public void SetCellAsVisited(int l, int c) {
        this.maze[l][c].visited = true;
    }

    public bool IsCellVisited(int l, int c) {
        return this.maze[l][c].visited;
    }

    public bool IsMazeVisited() {
        int i = 0;
        int j = 0;
        bool allVisited = true;

        while (allVisited && i < this.vSize) {
            if (!this.maze[i][j].visited) {
                allVisited = false;
            }
            j++;

            if (j>=this.hSize) {
                j=0;
                i++;
            }
        }

        return allVisited;
    }

    public int GetHSize() {
        return this.maze[0].Length;
    }

    public int GetVSize() {
        return this.maze.Length;
    }

    public void CreatePaths() {
        Random rand = new Random();
        int line = rand.Next(this.vSize);
        int column = rand.Next(this.hSize);

        while(!this.IsMazeVisited()){
            Direction direction = this.ChooseNeighbourDirection(line, column);
            int[] ncPos = this.FindNeighbourCellWithDirection(line, column, direction);
            
            if(!this.IsCellVisited(ncPos[0], ncPos[1])){
                this.OpenWall(line, column, direction);
                this.SetCellAsVisited(ncPos[0], ncPos[1]);
            }

            switch (direction) {
                case Direction.North :
                    line--;
                    break;
                case Direction.South :
                    line++;
                    break;
                case Direction.East :
                    column++;
                    break;
                case Direction.West :
                    column--;
                    break;
            }
        }
    }

    public void CreatePath(MazePath path) {
        List<(int, int)> indexSet = path.getIndexSet();
        List<Direction> directions = path.getDirections();

        // We open the 2 gates
        this.OpenTwoGateAt(indexSet[0], indexSet[indexSet.Count-1]);
        
        // We go through the path.indexSet and path.directions to open the according walls in the maze
        List<Direction>.Enumerator directionsIt = directions.GetEnumerator();

        foreach (var coord in indexSet) {
            directionsIt.MoveNext();
            this.OpenWall(coord.Item1, coord.Item2, directionsIt.Current);
            this.SetCellAsVisited(coord.Item1, coord.Item2);
        }
    }

    private Direction ChooseNeighbourDirection(int line, int column){
        Random r = new Random();
        bool[] possibleDirection = this.Neighbour(line, column);
        bool possible = false;
        int n = 0;
        while(!possible){
            n = r.Next(4);
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

    public bool encountersWallOnPath(MazePath path) {
        // We take enumerators because we want to stop the moment we find a wall
        List<(int, int)>.Enumerator indexIt = path.getIndexSet().GetEnumerator();
        List<Direction>.Enumerator directionsIt = path.getDirections().GetEnumerator();

        // We need to make sure we don't continue the while loop without a Current
        bool hasNext = indexIt.MoveNext() && directionsIt.MoveNext();

        // We also make sure to mark any wall we may find
        bool foundWall = false;

        while(hasNext && !foundWall) {
            // Invariant: Both MoveNext returned true and the path is opened until here
            int currentLine = indexIt.Current.Item1;
            int currentColumn = indexIt.Current.Item2;
            Direction currentDirection = directionsIt.Current;

            // If we have a Direction.None it will act as a wall but we don't want to
            if (currentDirection != Direction.None) {
                foundWall = !this.maze[currentLine][currentColumn].IsOpened(currentDirection);
            }

            hasNext = indexIt.MoveNext() && directionsIt.MoveNext();
        }
        // While stopped: we either went through the whole path or found a wall
        // In other words: If we found a wall we still have values (hasNext == true)

        return hasNext;
    }
}
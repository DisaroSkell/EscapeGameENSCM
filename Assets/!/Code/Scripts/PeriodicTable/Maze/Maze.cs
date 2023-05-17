using System;
using System.Collections.Generic;

/* Class for the creation and usage of a maze. */
public class Maze<Cell> where Cell : MazeCell, new() {
    // Horizontal size of the maze.
    private int hSize;

    // Vertical size of the maze.
    private int vSize;

    private int nbGates;

    // Max number of gates.
    private static readonly int maxNbGates = 5;

    public Cell[][] maze;
    
    /// <summary>
    /// Constructor of the maze. It does not create paths and gates.
    /// </summary>
    /// <param name="hSize">Horizontal size of the maze.</param>
    /// <param name="vSize">Vertical size of the maze.</param>
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
    }

    /// <summary>
    /// Creates random paths and gates for the maze.
    /// It only creates things if neeeded (so calling Explore() after another Explore() will do nothing).
    /// </summary>
    public void Explore() {
        this.CreatePaths();
        this.OpenTwoGate();
    }

    /// <summary>
    /// Creates a main path (and gates if it starts and/or ends at the maze border) for the maze.
    /// It then calls Explore(). (Explore will not open gates if the path opens them)
    /// </summary>
    /// <param name="path">The main path to create.</param>
    public void Explore(MazePath path) {
        this.CreatePath(path);
        this.Explore();
    }

    /// <summary>
    /// Opens two gates in the maze: one at the top left (to the north) and one at the bottom right (to the south).
    /// Opening gates should not make the gate count exceed maxNbGates.
    /// </summary>
    public void OpenTwoGate() {
        // The OpenGate((int, int)) method already does everything gate-related.
        this.OpenGate((0, 0));
        this.OpenGate((this.vSize - 1, this.hSize - 1));
    }

    /// <summary>
    /// Opens two gates in the maze at the given positions.
    /// Opening gates should not make the gate count exceed maxNbGates.
    /// </summary>
    /// <param name="gate1">Indexes (line, column) for position of the first gate.</param>
    /// <param name="gate2">Indexes (line, column) for position of the second gate.</param>
    public void OpenTwoGateAt((int, int) gate1, (int, int) gate2) {
        // The OpenGate((int, int)) method already does everything gate-related.
        this.OpenGate(gate1);
        this.OpenGate(gate2);
    }

    /// <summary>
    /// Opens a gate in the maze at the given position.
    /// Opening gates should not make the gate count exceed maxNbGates.
    /// If we select a corner position, it will choose North or South over East or West.
    /// </summary>
    /// <param name="gate">Indexes (line, column) for position of the gate.</param>
    public void OpenGate((int, int) gate) {
        Direction direction = Direction.None;
        
        // We pick the direction from the position.
        if (gate.Item2 == 0) direction = Direction.West;
        if (gate.Item2 == hSize-1) direction = Direction.East;

        // North and South have the upper hand on East and West.
        if (gate.Item1 == 0) direction = Direction.North;
        if (gate.Item1 == vSize-1) direction = Direction.South;

        // If direction == Direction.None here, then we'll not open anything since we are not at the border.

        // The OpenWall(int, int, direction) method already does everything gate-related.
        OpenWall(gate.Item1, gate.Item2, direction);
    }

    public int GetNbGates() {
        return this.nbGates;
    }

    /// <summary>
    /// Opens a wall at the given position and direction.
    /// If it leads to the outside of the maze, it counts as a gate.
    /// It will not open the wall if it's a gate and there are already maxNbGates.
    /// </summary>
    /// <param name="l">Line of the position.</param>
    /// <param name="c">Column of the position.</param>
    /// <param name="direction">Direction where we open.</param>
    public void OpenWall(int l, int c, Direction direction) {
        if (((l == 0 && direction == Direction.North) || (l == vSize-1 && direction == Direction.South) || (c == 0 && direction == Direction.West) || (c == hSize-1 && direction == Direction.East))) {
            if (this.GetNbGates() >= Maze<Cell>.maxNbGates) {
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

    /// <summary>
    /// Looks at neighbours cells to see if they exist.
    /// </summary>
    /// <param name="l">Line of the position.</param>
    /// <param name="c">Column of the position.</param>
    /// <returns>
    /// Array of boolean that each indicates if there is a neighbour cell.
    /// [0] for North.
    /// [1] for South.
    /// [2] for West.
    /// [3] for East.
    /// </returns>
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

    /// <summary>
    /// Calculates the coordinates of the neighbour cell given the position and the direction to look at.
    /// </summary>
    /// <param name="l">Line of the position.</param>
    /// <param name="c">Column of the position.</param>
    /// <param name="direction">Direction to look at.</param>
    /// <returns>
    /// Array of boolean that each indicates if there is a neighbour cell.
    /// [0] for line.
    /// [1] for column.
    /// </returns>
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

    /// <summary>
    /// Maze is visited when all cells in it are marked as visited.
    /// </summary>
    /// <returns>
    /// Returns false if a cell that is not visited is encountered.
    /// Else it retruns true.
    /// </returns>
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

    /// <summary>
    /// Creates random paths for the maze.
    /// It visits all the cells and break walls to fully build the maze.
    /// </summary>
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

    /// <summary>
    /// Creates a path in the maze.
    /// It visits all the cells and break walls on the way.
    /// It opens a wall at the start and the end of the given path.
    /// </summary>
    /// <param name="path">Path to create.</param>
    public void CreatePath(MazePath path) {
        List<(int, int)> indexSet = path.GetIndexSet();
        List<Direction> directions = path.GetDirections();

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

    /// <summary>
    /// Used to choose a direction at random.
    /// </summary>
    /// <param name="line">Line of the position.</param>
    /// <param name="column">Column of the position.</param>
    /// <param name="direction">Direction where we open.</param>
    /// <returns>
    /// Returns a random direction between North, East, South and West.
    /// </returns>
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

    /// <summary>
    /// Check if there is a wall on the given path.
    /// </summary>
    /// <param name="path">Path to check.</param>
    /// <returns>
    /// Returns true if a wall was encountered.
    /// Else returns false.
    /// </returns>
    public bool EncountersWallOnPath(MazePath path) {
        // We take enumerators because we want to stop the moment we find a wall
        List<(int, int)>.Enumerator indexIt = path.GetIndexSet().GetEnumerator();
        List<Direction>.Enumerator directionsIt = path.GetDirections().GetEnumerator();

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
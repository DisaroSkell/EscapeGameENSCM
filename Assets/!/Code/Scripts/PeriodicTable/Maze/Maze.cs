using System;

public class Maze {
    private int hSize;

    private int vSize;

    private int nbGates;

    public MazeCell[][] maze;
    public PathBuilder pathBuilder;

    public Maze(int hSize, int vSize) {
        this.hSize = hSize;
        this.vSize = vSize;

        this.nbGates = 0;
        this.maze = new MazeCell[vSize][];

        for (int i = 0; i < vSize; i++) {
            this.maze[i] = new MazeCell[hSize];

            for (int j = 0; j < hSize; j++) {
                this.maze[i][j] = new MazeCell();
            }
        }

        Random rand = new Random();
        this.pathBuilder = new PathBuilder(rand.Next(vSize), rand.Next(hSize));
    }

    public void Explore() {
        this.pathBuilder.CreatePaths(this);
        this.OpenTwoGate();
    }

    public void Explore(MazePath path) {
        this.pathBuilder.CreatePath(this, path);
        this.pathBuilder.CreatePaths(this);
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

    public MazeCell GetCellAt(int l, int c) {
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
}
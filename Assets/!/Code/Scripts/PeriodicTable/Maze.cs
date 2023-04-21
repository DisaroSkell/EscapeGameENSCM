using System;

public class Maze {
    private int size {
        get {
            return maze.Length;
        }
    }

    private int nbGates;

    public MazeCell[][] maze;
    public WallBreaker wallBreaker;

    public Maze(int hSize, int vSize) {
        maze = new MazeCell[vSize][];

        for (int i = 0; i < vSize; i++) {
            maze[i] = new MazeCell[hSize];

            for (int j = 0; j < hSize; j++) {
                maze[i][j] = new MazeCell();
            }
        }
    }

    public void Explore() {
        this.wallBreaker.CreatePaths(this);
        this.OpenTwoEntry();
    }

    public void OpenTwoEntry() {
        this.OpenWall(0, 0, 'W');
        this.OpenWall(this.size - 1, this.size - 1, 'E');
    }

    public int GetNbGates() {
        return this.nbGates;
    }

    public void OpenWall(int l, int c, char direction) {
        if (((l == 0 && direction == 'N') || (l == size-1 && direction == 'S') || (c == 0 && direction == 'W') || (c == size-1 && direction == 'E'))) {
            if (this.GetNbGates() >= 2) {
                return;
            }
            this.nbGates += 1;
        }
        this.maze[l][c].OpenWall(direction);
        switch (direction) {
            case 'N' :
                if (l > 0){
                    this.maze[l-1][c].OpenWall('S');
                }
                break;
            case 'S' :
                if (l < this.size - 1){
                    this.maze[l+1][c].OpenWall('N');
                }
                break;
            case 'E' :
                if (c < this.size - 1){
                    this.maze[l][c+1].OpenWall('W');
                }
                break;
            case 'W' :
                if (c > 0){
                    this.maze[l][c-1].OpenWall('E');
                }
                break;
        }
    }

    public bool IsOpened(int l, int c, char direction) {
        return this.maze[l][c].IsOpened(direction);
    }

    public MazeCell GetCopyOfCell(int l, int c) {
        return this.maze[l][c].Copy();
    }

    public bool[] Neighbour(int l, int c) {
        bool[] n = new bool[4];
        for (int i = 0; i < 4; i++) {
            n[i] = false;
        }
        if (l > 0) {
            n[0] = true;
        }
        if (l < this.size - 1) {
            n[1] = true;
        }
        if (c > 0) {
            n[3] = true;
        }
        if (c < this.size - 1) {
            n[2] = true;
        }
        return n;
    }

    public int[] FindNeighbourCellWithDirection(int l, int c, char direction) {
        int[] tmp = {l, c};

        switch (direction) {
            case 'N' :
                tmp[0] -= 1;
                break;
            case 'S' :
                tmp[0] += 1;
                break;
            case 'E' :
                tmp[1] += 1;
                break;
            case 'W' :
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

        while (allVisited && i < this.size) {
            if (!this.maze[i][j].visited) {
                allVisited = false;
            }
            j++;

            if (j>=this.size) {
                j=0;
                i++;
            }
        }

        return allVisited;
    }

    public int GetSize() {
        return this.size;
    }

    public int GetBreakerLine() {
        return this.wallBreaker.GetLine();
    }

    public int GetBreakerColumn() {
        return this.wallBreaker.GetColumn();
    }
}
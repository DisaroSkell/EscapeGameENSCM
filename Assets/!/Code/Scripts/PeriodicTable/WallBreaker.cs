using System;

public class WallBreaker {
    private int line;
    private int column;

    public WallBreaker(int l, int c){
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
            char direction = ChooseNeighbourDirection(maze);
            int[] ncPos = maze.FindNeighbourCellWithDirection(this.GetColumn(), this.GetLine(), direction);
            
            if(!maze.IsCellVisited(ncPos[0], ncPos[1])){
                maze.OpenWall(this.GetColumn(), this.GetLine(), direction);
                maze.SetCellAsVisited(ncPos[0], ncPos[1]);
            }

            switch (direction) {
                case 'N' :
                    this.SetColumn(this.GetColumn() - 1);
                    break;
                case 'S' :
                    this.SetColumn(this.GetColumn() + 1);
                    break;
                case 'E' :
                    this.SetLine(this.GetLine() + 1);
                    break;
                case 'W' :
                    this.SetLine(this.GetLine() - 1);
                    break;
            }
        }
    }

    private char ChooseNeighbourDirection(Maze maze){
        Random r = new Random();
        bool[] possibleDirection = maze.Neighbour(this.GetColumn(), this.GetLine());
        bool possible = false;
        int n = 0;
        while(!possible){
            n = r.Next(1, 5);
            possible = possibleDirection[n];
        }

        switch(n){
            case 0 : return 'N';
            case 1 : return 'S';
            case 2 : return 'E';
            case 3 : return 'W';
        }

        return ' ';
    }
}
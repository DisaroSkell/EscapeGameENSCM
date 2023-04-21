public class MazeCell {
    public bool north;
    public bool east;
    public bool west;
    public bool south;

    // mark if the cell has been visited by the wallBreaker
    public bool visited;

    public MazeCell() {
        this.north = true;
        this.east = true;
        this.west = true;
        this.south = true;
        this.visited = false;
    }

    public bool IsClosed() {
        return(this.north && this.south && this.east && this.west);
    }

    public void OpenWall(char direction) {
        switch(direction){
            case 'N' :
                this.north = false;
                break;
            case 'S' :
                this.south = false;
                break;
            case 'E' :
                this.east = false;
                break;
            case 'W' :
                this.west = false;
                break;
        }
    }

    public bool IsOpened(char direction) {
        switch(direction){
            case 'N':
                return !this.north;
            case 'S':
                return !this.south;
            case 'E':
                return !this.east;
            case 'W':
                return !this.west;
            default :
                return false;
        }
    }

    public MazeCell Copy() {
        MazeCell cell = new MazeCell();
        cell.north = this.north;
        cell.south = this.south;
        cell.east = this.east;
        cell.west = this.west;
        return cell;
    }
}

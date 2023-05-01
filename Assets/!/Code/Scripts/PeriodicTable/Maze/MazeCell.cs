public class MazeCell {
    public bool north;
    public bool east;
    public bool west;
    public bool south;

    // mark if the cell has been visited when building
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

    public void OpenWall(Direction direction) {
        switch(direction){
            case Direction.North :
                this.north = false;
                break;
            case Direction.South :
                this.south = false;
                break;
            case Direction.East :
                this.east = false;
                break;
            case Direction.West :
                this.west = false;
                break;
        }
    }

    public bool IsOpened(Direction direction) {
        switch(direction){
            case Direction.North :
                return !this.north;
            case Direction.South :
                return !this.south;
            case Direction.East :
                return !this.east;
            case Direction.West :
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

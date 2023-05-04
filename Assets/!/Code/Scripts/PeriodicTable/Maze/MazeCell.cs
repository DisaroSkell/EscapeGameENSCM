/* Class for a cell of the maze. */
public class MazeCell {
    // True if there is a wall to the north.
    public bool north;

    // True if there is a wall to the east.
    public bool east;

    // True if there is a wall to the west.
    public bool west;
    
    // True if there is a wall to the south.
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

    /// <summary>
    /// A cell is call closed when all its sides have walls.
    /// </summary>
    /// <returns>
    /// Returns true if all 4 of the direction variables are true.
    /// Else returns false.
    /// </returns>
    public bool IsClosed() {
        return(this.north && this.south && this.east && this.west);
    }


    /// <summary>
    /// Setter for the walls of the cell.
    /// </summary>
    /// <param name="direction">Direction of the wall to open.</param>
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

    /// <summary>
    /// Getter for the walls of the cell.
    /// </summary>
    /// <param name="direction">Direction of the wall to check.</param>
    /// <returns>
    /// Returns true if there is a wall in the given direction.
    /// Else returns false.
    /// </returns>
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
}

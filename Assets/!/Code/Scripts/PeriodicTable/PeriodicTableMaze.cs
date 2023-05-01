public class PeriodicTableMaze {
    private Maze<MazeCell> maze;

    public PeriodicTableMaze((int, int)[] atoms, Direction[] arrows) {
        this.maze = new Maze<MazeCell>(18, 9);

        this.maze.Explore(new MazePath(atoms, arrows));
    }

    public Maze<MazeCell> GetMaze() {
        return this.maze;
    }
}
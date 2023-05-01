using System;

public class WaySheet {
    public void CreateWaySheet(int nbAtoms) {
        (int, int)[] atoms = PickRandomAtoms(nbAtoms);
        Direction[] directions = GenerateArrows(atoms);

        CreateWaySheet(atoms, directions);
    }

    public void CreateWaySheet((int, int)[] atoms, Direction[] directions) {
        PeriodicTableMaze periodicTable = new PeriodicTableMaze(atoms, directions);

        // TODO : output
    }

    public (int, int)[] PickRandomAtoms(int count) {
        (int, int)[] atoms = new (int, int)[count];
        Random rand = new Random();

        for (int i = 0; i < count; i++) {
            int line = 0;
            int column = 0;

            do {
                line = rand.Next(9);
                column = rand.Next(18);
            } while (Array.Exists(atoms, coord => coord == (line, column)));

            atoms[i] = (line, column);
        }

        return atoms;
    }

    public Direction[] GenerateArrows((int, int)[] atoms) {
        Direction[] directions = new Direction[atoms.Length];

        for (int i = 0; i < atoms.Length-1; i++) {
            if (this.AreAtomsAligned(atoms[i], atoms[i+1])) {
                directions[i] = Direction.None;
            } else {
                // TODO
                // Complete on randomize implementation

                // Here we need to check the relative position of the next atom
                // We need to make sure that the direction we are giving doesn't make the path cross itself

                if (atoms[i].Item1 > atoms[i].Item1) {
                    directions[i] = Direction.North;
                } else {
                    directions[i] = Direction.South;
                }
            }
        }

        return directions;
    }

    private bool AreAtomsAligned((int, int) atom1, (int, int) atom2) {
        return (atom1.Item1 == atom2.Item1 || atom1.Item2 == atom2.Item2);
    }
}

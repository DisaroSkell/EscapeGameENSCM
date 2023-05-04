using System;

/* Class for creation of a sheet that has information for resolving the periodic table maze. */
public class WaySheet {
    /// <summary>
    /// Creates the sheet by picking atoms at random.
    /// </summary>
    /// <param name="nbAtoms">Number of atoms we want to pick.</param>
    public void CreateWaySheet(int nbAtoms) {
        (int, int)[] atoms = PickRandomAtoms(nbAtoms);
        Direction[] directions = GenerateArrows(atoms);

        CreateWaySheet(atoms, directions);
    }

    /// <summary>
    /// Creates the sheet with given atoms and directions.
    /// </summary>
    /// <param name="atoms">Array of indexes corresponding to the position of each atom in the periodic table maze.</param>
    /// <param name="directions">Array of directions to go from each atom to the other. It is supposed to be the same size of atoms.</param>
    public void CreateWaySheet((int, int)[] atoms, Direction[] directions) {
        // PeriodicTableMaze periodicTable = new PeriodicTableMaze(atoms, directions);

        // TODO : output
    }

    /// <summary>
    /// Selects atoms at random and returns them. There should be no duplicates.
    /// </summary>
    /// <param name="count">Number of atoms to pick.</param>
    /// <returns>
    /// Returns the atoms picked at random.
    /// </returns>
    public (int, int)[] PickRandomAtoms(int count) {
        (int, int)[] atoms = new (int, int)[count];
        Random rand = new Random();

        for (int i = 0; i < count; i++) {
            int line = 0;
            int column = 0;

            // Make sure to pick an atom that hasn't been picked yet.
            do {
                line = rand.Next(9);
                column = rand.Next(18);
            } while (Array.Exists(atoms, coord => coord == (line, column)));

            atoms[i] = (line, column);
        }

        return atoms;
    }

    /// <summary>
    /// Selects arrows to get from an atom to the other. The path created with the arrows should not cross himself.
    /// </summary>
    /// <param name="atoms">Array of indexes corresponding to the position of the atoms we want to link.</param>
    /// <returns>
    /// Returns the arrows selected.
    /// </returns>
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

    /// <summary>
    /// Checks if atoms are aligned in the periodic table.
    /// </summary>
    /// <param name="atom1">Index corresponding to the position of the first atom.</param>
    /// <param name="atom2">Index corresponding to the position of the second atom.</param>
    /// <returns>
    /// Returns true if the atoms are aligned.
    /// Else returns false.
    /// </returns>
    private bool AreAtomsAligned((int, int) atom1, (int, int) atom2) {
        return (atom1.Item1 == atom2.Item1 || atom1.Item2 == atom2.Item2);
    }
}

using UnityEngine;
using UnityEngine.EventSystems;

/* Class for the bottom element of the periodic table maze. */
public class BottomElement : TableElement {
    /// <summary>
    /// Function to call as a constructor just after instantiation.
    /// It sets line and column to (-1, -1).
    /// </summary>
    /// <param name="line">Line of the element in the maze.</param>
    /// <param name="column">Column of the element in the maze.</param>
    public override void Initialize(int line, int column) {
        base.Initialize(-1, -1);
    }

    /// <summary>
    /// Function to call as a constructor just after instantiation.
    /// It sets line and column to (-1, -1).
    /// </summary>
    public void Initialize() {
        base.Initialize(-1, -1);
    }
}

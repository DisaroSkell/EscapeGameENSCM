using UnityEngine;
using UnityEngine.EventSystems;

/* Class for an element of the periodic table maze. */
public class TableElement : MonoBehaviour, IPointerClickHandler {
    // Index for the position of the element in the maze.
    private (int, int) index;

    /// <summary>
    /// Function to call as a constructor just after instantiation.
    /// </summary>
    /// <param name="line">Line of the element in the maze.</param>
    /// <param name="column">Column of the element in the maze.</param>
    public void Initialize(int line, int column) {
        this.index = (line, column);
    }

    /// <summary>
    /// Fetches the magnet to the parent which is supposed to be the Periodic Table Maze.
    /// </summary>
    /// <returns>
    /// Returns the magnet if the table has one attached to it.
    /// Else returns null.
    /// </returns>
    public MagnetInteractions GetMagnet() {
        PeriodicTableMaze table = this.GetComponentInParent<PeriodicTableMaze>();

        if (table is not null) {
            return table.magnet;
        }

        return null;
    }

    /// <summary>
    /// Function of the IPointerClickHandler interface.
    /// Makes the magnet go to this element's position if magnet is aligned to it.
    /// Does nothing if there are no magnet on the table.
    /// </summary>
    /// <param name="PointerEventData">Unity class that contains information about a pointer event.</param>
    public void OnPointerClick(PointerEventData eventData) {
        MagnetInteractions magnet = GetMagnet();

        if (magnet is not null) {
            (int, int) magnetPos = magnet.GetCellPosition();
            if (magnetPos.Item1 == this.index.Item1) {
                magnet.HorizontalMouvement(this.index.Item2);
            } else if (magnetPos.Item2 == this.index.Item2) {
                magnet.VerticalMouvement(this.index.Item1);
            }
        }
    }
}

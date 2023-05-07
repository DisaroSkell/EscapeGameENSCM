using UnityEngine;
using UnityEngine.EventSystems;

/* Class for the top element of the periodic table maze. */
public class TopElement : TableElement {
    /// <summary>
    /// Function to call as a constructor just after instantiation.
    /// It sets line and column to (-2, -2).
    /// </summary>
    /// <param name="line">Line of the element in the maze.</param>
    /// <param name="column">Column of the element in the maze.</param>
    public override void Initialize(int line, int column) {
        base.Initialize(-2, -2);
    }

    /// <summary>
    /// Function to call as a constructor just after instantiation.
    /// It sets line and column to (-2, -2).
    /// </summary>
    public void Initialize() {
        base.Initialize(-2, -2);
    }

    /// <summary>
    /// Function of the IPointerClickHandler interface.
    /// Makes the magnet go to the top of the table.
    /// </summary>
    /// <param name="PointerEventData">Unity class that contains information about a pointer event.</param>
    public override void OnPointerClick(PointerEventData eventData) {
        MagnetInteractions magnet = GetMagnet();
        if(magnet is null) return;
        
        (int, int) magnetPos = magnet.GetCellPosition();
        if (magnetPos != (-1, -1)) {
            magnet.ToTopMouvement();
        }
    }
}

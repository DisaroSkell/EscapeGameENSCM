using UnityEngine;
using UnityEngine.EventSystems;

public class TableElement : MonoBehaviour, IPointerClickHandler {
    private (int, int) index;

    public void Initialize(int line, int column) {
        this.index = (line, column);
    }

    public MagnetInteractions GetMagnet() {
        PeriodicTableMaze table = this.GetComponentInParent<PeriodicTableMaze>();

        if (table is not null) {
            return table.magnet;
        }

        return null;
    }

    public void OnPointerClick(PointerEventData eventData) {
        MagnetInteractions magnet = GetMagnet();

        if (magnet is not null) {
            (int, int) magnetPos = magnet.GetCellPosition();
            if (magnetPos.Item1 == this.index.Item1) {
                magnet.horizontalMouvement(this.index.Item2);
            } else if (magnetPos.Item2 == this.index.Item2) {
                magnet.verticalMouvement(this.index.Item1);
            }
        }
    }
}

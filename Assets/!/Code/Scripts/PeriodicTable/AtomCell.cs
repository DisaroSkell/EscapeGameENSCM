using UnityEngine;

/* Class for an atom cell to add a position to the MazeCell. */
public class AtomCell : MazeCell {
    public Vector3 position;

    private GameObject glowElem;

    public void setGlower(GameObject glower) {
        this.glowElem = glower;
    }

    public void GlowOn() {
        if (this.glowElem is not null) {
            this.glowElem.SetActive(true);
        }
    }

    public void GlowOff() {
        if (this.glowElem is not null) {
            this.glowElem.SetActive(false);
        }
    }
}
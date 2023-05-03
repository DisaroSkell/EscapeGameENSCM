using System;
using UnityEngine;

public class PeriodicTableMaze : MonoBehaviour {
    public float atomSize;

    // Position of the first Atom of the table (relative to the parent)
    public Vector3 firstAtomPosition;

    public float hSpacing;

    public float vSpacing;

    public TableElement atomPrefab;

    public Quaternion rotation;

    public Texture defaultAtomImage;

    public string atomImageFolder;

    public MagnetInteractions magnet;

    public MagnetInteractions magnetPrefab;
    public float magnetSize;
    public Quaternion magnetOrientation;

    public KeyInteractions key;
    public KeyInteractions keyPrefab;
    public Vector3 relativeDefaultKeyPosition;
    public float keySize;
    public Quaternion keyOrientation;

    private bool[][] isCellBlank;

    private Maze<AtomCell> maze;

    public void Start() {
        // TODO randomize those
        (int, int)[] atomsIndex = {
                                    (8, 6),     // Uranium
                                    (7, 9),     // Europium
                                    (5, 9),     // Platinium
                                    (5, 13),    // Lead
                                    (4, 13),    // Tin
                                    (4, 16),    // Iodine
                                    (2, 16),    // Chlorine
                                    (2, 12),    // Aluminium
                                    (3, 7),     // Iron
                                    (5, 5),     // Tungsten
                                    (3, 5),     // Chromium
                                    (3, 3),     // Titanium
                                    (3, 0),     // Potassium
                                    (1, 0),     // Lithium
                                    (1, 14)     // Nitrogen
                                  };
        Direction[] arrows = {
                                Direction.East,     // Uranium => Europium
                                Direction.None,     // Europium => Platinium
                                Direction.None,     // Platinium => Lead
                                Direction.None,     // Lead => Tin
                                Direction.None,     // Tin => Iodine
                                Direction.None,     // Iodine => Chlorine
                                Direction.None,     // Chlorine => Aluminium
                                Direction.South,    // Aluminium => Iron
                                Direction.South,    // Iron => Tungsten
                                Direction.None,     // Tungsten => Chromium
                                Direction.None,     // Chromium => Titanium
                                Direction.None,     // Titanium => Potassium
                                Direction.None,     // Potassium => Lithium
                                Direction.None,     // Lithium => Nitrogen
                                Direction.North     // Nitrogen => Exit
                             };

        this.isCellBlank = new bool[9][];

        for (int i = 0; i < 9; i++) {
            this.isCellBlank[i] = new bool[18];

            for (int j = 0; j < 18; j++) {
                this.isCellBlank[i][j] = false;
            }
        }

        this.isCellBlank[0][1] = true;
        this.isCellBlank[0][2] = true;
        this.isCellBlank[0][3] = true;
        this.isCellBlank[0][4] = true;
        this.isCellBlank[0][5] = true;
        this.isCellBlank[0][6] = true;
        this.isCellBlank[0][7] = true;
        this.isCellBlank[0][8] = true;
        this.isCellBlank[0][9] = true;
        this.isCellBlank[0][10] = true;
        this.isCellBlank[0][11] = true;
        this.isCellBlank[0][12] = true;
        this.isCellBlank[0][13] = true;
        this.isCellBlank[0][14] = true;
        this.isCellBlank[0][15] = true;
        this.isCellBlank[0][16] = true;
        
        this.isCellBlank[1][2] = true;
        this.isCellBlank[1][3] = true;
        this.isCellBlank[1][4] = true;
        this.isCellBlank[1][5] = true;
        this.isCellBlank[1][6] = true;
        this.isCellBlank[1][7] = true;
        this.isCellBlank[1][8] = true;
        this.isCellBlank[1][9] = true;
        this.isCellBlank[1][10] = true;
        this.isCellBlank[1][11] = true;
        
        this.isCellBlank[2][2] = true;
        this.isCellBlank[2][3] = true;
        this.isCellBlank[2][4] = true;
        this.isCellBlank[2][5] = true;
        this.isCellBlank[2][6] = true;
        this.isCellBlank[2][7] = true;
        this.isCellBlank[2][8] = true;
        this.isCellBlank[2][9] = true;
        this.isCellBlank[2][10] = true;
        this.isCellBlank[2][11] = true;
        
        this.isCellBlank[5][2] = true;

        this.isCellBlank[6][2] = true;
        
        this.isCellBlank[7][0] = true;
        this.isCellBlank[7][1] = true;
        this.isCellBlank[7][2] = true;
        
        this.isCellBlank[8][0] = true;
        this.isCellBlank[8][1] = true;
        this.isCellBlank[8][2] = true;

        this.maze = new Maze<AtomCell>(18, 9);
        this.maze.Explore(new MazePath(atomsIndex, arrows));
        
        CreateAtoms();
    }

    public void CreateAtoms() {
        Texture[] atomImages = Resources.LoadAll<Texture>(atomImageFolder);

        int imageCounter = 0;

        for (int i = 0; i < this.maze.maze.Length; i++) {
            for (int j = 0; j < this.maze.maze[i].Length; j++) {
                Vector3 atomPos = new Vector3((float)(this.transform.position.x + j*(this.atomSize + this.hSpacing)),
                                              (float)(this.transform.position.y - i*(this.atomSize + this.vSpacing)),
                                              this.transform.position.z);

                atomPos += this.firstAtomPosition;

                TableElement atom = (TableElement)Instantiate(this.atomPrefab, atomPos, Quaternion.identity, this.transform);

                atom.Initialize(i, j);

                atom.transform.localScale = new Vector3(atomSize, atomSize, 1);

                this.maze.GetCellAt(i, j).position = atom.transform.position;

                if (imageCounter < atomImages.Length && !this.isCellBlank[i][j]) {
                    atom.GetComponent<Renderer>().material.mainTexture = atomImages[imageCounter];

                    /* int underscoreIndex = atomImages[imageCounter].name.IndexOf("_");
                    atom.name = atomImages[imageCounter].name.Substring(underscoreIndex + 1); */

                    imageCounter++;
                } else {
                    atom.GetComponent<Renderer>().material.mainTexture = this.defaultAtomImage;
                    atom.name = "blank" + i + "_" + j;
                }
            }
        }

        Vector3 keyPos = new Vector3((float)this.transform.position.x + this.relativeDefaultKeyPosition.x,
                                     (float)this.transform.position.y + this.relativeDefaultKeyPosition.y,
                                     (float)this.transform.position.z + this.relativeDefaultKeyPosition.z - this.keySize);
        this.key = (KeyInteractions)Instantiate(this.keyPrefab, keyPos, this.keyOrientation, this.transform);

        // TODO Remove this
        Vector3 magnetPos = new Vector3((float)this.transform.position.x + this.firstAtomPosition.x,
                                        (float)this.transform.position.y + this.firstAtomPosition.y,
                                        (float)this.transform.position.z + this.firstAtomPosition.z - this.magnetSize);
        this.magnet = (MagnetInteractions)Instantiate(this.magnetPrefab, magnetPos, this.magnetOrientation, this.transform);

        this.transform.rotation *= this.rotation;
    }

    public Maze<AtomCell> GetMaze() {
        return this.maze;
    }

    public void makeKeyFall() {
        this.key.transform.localPosition = this.relativeDefaultKeyPosition;
    }
}
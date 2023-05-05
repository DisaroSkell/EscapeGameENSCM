using System;
using UnityEngine;

/* Class for the Periodic Table which also happens to be a maze. */
public class PeriodicTableMaze : MonoBehaviour {
    // Size of an element of the periodic table.
    public float atomSize;

    // Position of the first Atom of the table (relative to the parent).
    public Vector3 firstAtomPosition;

    // Horizontal spacing between elements of the periodic table.
    public float hSpacing;

    // Vertical spacing between elements of the periodic table.
    public float vSpacing;

    // Prefab of an element of the periodic table.
    public TableElement atomPrefab;

    // Rotation of the whole table.
    public Quaternion rotation;

    // Default atom image, used as a blank in the periodic table.
    public Texture defaultAtomImage;

    // Name of the folder where the images of the atoms are stored in.
    public string atomImageFolder;

    // Prefab for the top element 
    public TopElement topPrefab;
    // Size for the top element 
    public float topVerticalSize;

    // Prefab for the bottom element 
    public BottomElement bottomPrefab;
    // Size for the bottom element 
    public float bottomVerticalSize;

    // Magnet attached to the table. Can be null.
    public MagnetInteractions magnet;

    public MagnetInteractions magnetPrefab;
    public float magnetSize;
    public Quaternion magnetOrientation;

    // Key attached to the table. Can be null.
    public KeyInteractions key;
    
    public KeyInteractions keyPrefab;
    // Relative position of the key when it is unattached to the magnet.
    public Vector3 relativeDefaultKeyPosition;
    public float keySize;
    public Quaternion keyOrientation;

    // The maze behind the table.
    private Maze<AtomCell> maze;

    // Boolean tab that marks all the cells of the maze attribute. true = blank cell ; false = cell that contains an atom
    private bool[][] isCellBlank;

    // Position of the top element.
    private Vector3 topPosition;
    // Position of the bottom element.
    private Vector3 bottomPosition;

    /// <summary>
    /// Unity Start method. Called at the start of the game.
    /// It creates the whole maze and the display of the periodic table with prefabs.
    /// </summary>
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
                                    (1, 14),    // Nitrogen
                                    (0, 14)     // Exit Cell
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
                                Direction.North,    // Nitrogen => Exit
                                Direction.None      // Exit =>
                             };

        // Blank cells (yes I did it one by one)
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

    /// <summary>
    /// Creates the periodic table display with the prefabs.
    /// It also generates the key.
    /// </summary>
    private void CreateAtoms() {
        // Retrieve images from the given folder.
        Texture[] atomImages = Resources.LoadAll<Texture>(atomImageFolder);

        // We count used image to access the correct image and avoid array overflow.
        int imageCounter = 0;

        for (int i = 0; i < this.maze.GetVSize(); i++) {
            for (int j = 0; j < this.maze.GetHSize(); j++) {
                // The (i, j) atom's position is calculated with its index position, the first atom position, the atom size, and the spacing.
                Vector3 atomPos = new Vector3((float)(this.transform.position.x + j*(this.atomSize + this.hSpacing)),
                                              (float)(this.transform.position.y - i*(this.atomSize + this.vSpacing)),
                                              this.transform.position.z);

                atomPos += this.firstAtomPosition;

                TableElement atom = (TableElement)Instantiate(this.atomPrefab, atomPos, Quaternion.identity, this.transform);

                atom.Initialize(i, j);

                // We scale the atom with the given size.
                atom.transform.localScale = new Vector3(this.atomSize, this.atomSize, 1);

                this.maze.GetCellAt(i, j).position = atom.transform.position;

                // We make sure to put the correct image and name on the atom.
                if (imageCounter < atomImages.Length && !this.isCellBlank[i][j]) {
                    atom.GetComponent<Renderer>().material.mainTexture = atomImages[imageCounter];

                    /* int underscoreIndex = atomImages[imageCounter].name.IndexOf("_");
                    atom.name = atomImages[imageCounter].name.Substring(underscoreIndex + 1); */

                    // We increment the counter only if we used an image.
                    imageCounter++;
                } else {
                    atom.GetComponent<Renderer>().material.mainTexture = this.defaultAtomImage;
                    atom.name = "blank" + i + "_" + j;
                }
            }
        }

        // Horizontal average position.
        float middlePos = (this.maze.GetCellAt(0, 0).position.x + this.maze.GetCellAt(0, this.maze.GetHSize()-1).position.x)/2;

        this.InitTop(middlePos);
        
        this.InitBottom(middlePos);

        this.InitKey();

        // TODO Remove this (magnet will be instantiated with an interaction)
        this.InitMagnet();

        // We rotate the whole table with given Quaternion.
        this.transform.rotation *= this.rotation;
    }

    /// <summary>
    /// Instantiates the top element.
    /// </summary>
    /// <param name="middlePos">The horizontal center of the table.</param>
    public void InitTop(float middlePos) {
        // Top element instanciation.
        this.topPosition = new Vector3((float)(middlePos),
                                      (float)(this.maze.GetCellAt(0, 0).position.y + this.topVerticalSize/2 + this.atomSize/2 + this.vSpacing),
                                      this.transform.position.z);

        TopElement top = (TopElement)Instantiate(this.topPrefab, this.topPosition, Quaternion.identity, this.transform);

        top.Initialize();

        // The top element is taking the whole horizontal space.
        float xTopScale = this.maze.GetHSize()*this.atomSize + (this.maze.GetHSize()-1)*this.hSpacing;

        top.transform.localScale = new Vector3(xTopScale, this.topVerticalSize, 1);

        // Top element image and naming.
        top.GetComponent<Renderer>().material.mainTexture = this.defaultAtomImage;
        top.name = "TopElement";
    }

    /// <summary>
    /// Instantiates the bottom element.
    /// </summary>
    /// <param name="middlePos">The horizontal center of the table.</param>
    public void InitBottom(float middlePos) {
        // Bottom element instanciation.
        this.bottomPosition = new Vector3((float)(middlePos),
                                        (float)(this.maze.GetCellAt(this.maze.GetVSize()-1, 0).position.y - (this.bottomVerticalSize/2 + this.atomSize/2 + this.vSpacing)),
                                        this.transform.position.z);

        BottomElement bottom = (BottomElement)Instantiate(this.bottomPrefab, this.bottomPosition, Quaternion.identity, this.transform);

        bottom.Initialize();

        // The bottom element is taking the whole horizontal space.
        float xBottomScale = this.maze.GetHSize()*this.atomSize + (this.maze.GetHSize()-1)*this.hSpacing;

        bottom.transform.localScale = new Vector3(xBottomScale, this.bottomVerticalSize, 1);

        // Bottom element image and naming.
        bottom.GetComponent<Renderer>().material.mainTexture = this.defaultAtomImage;
        bottom.name = "BottomElement";
    }

    /// <summary>
    /// Instantiates a key at the bottom of the table.
    /// </summary>
    public void InitKey() {
        Vector3 keyPos = new Vector3((float)this.transform.position.x + this.relativeDefaultKeyPosition.x,
                                     (float)this.transform.position.y + this.relativeDefaultKeyPosition.y,
                                     (float)this.transform.position.z + this.relativeDefaultKeyPosition.z - this.keySize);
        this.key = (KeyInteractions)Instantiate(this.keyPrefab, keyPos, this.keyOrientation, this.transform);
    }

    /// <summary>
    /// Instantiates a magnet at the first atom position.
    /// </summary>
    public void InitMagnet() {
        Vector3 magnetPos = new Vector3((float)this.transform.position.x + this.firstAtomPosition.x,
                                        (float)this.transform.position.y + this.firstAtomPosition.y,
                                        (float)this.transform.position.z + this.firstAtomPosition.z - this.magnetSize);
        this.magnet = (MagnetInteractions)Instantiate(this.magnetPrefab, magnetPos, this.magnetOrientation, this.transform);
    }

    public Maze<AtomCell> GetMaze() {
        return this.maze;
    }

    public Vector3 GetTopPosition() {
        return this.topPosition;
    }

    public Vector3 GetBottomPosition() {
        return this.bottomPosition;
    }

    /// <summary>
    /// Makes the key return to its initial position.
    /// </summary>
    public void MakeKeyFall() {
        this.key.transform.localPosition = this.relativeDefaultKeyPosition;
    }
}
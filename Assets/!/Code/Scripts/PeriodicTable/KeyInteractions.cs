using UnityEngine;

/* Class for interactions with the key */
public class KeyInteractions : MonoBehaviour {
    // Position of the key in the maze.
    //(-1, -1) corresponds to the bottom of the maze.
    //(-2, -2) corresponds to the top of the maze.
    public (int, int) indexPosition = (-1, -1);
}
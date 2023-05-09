/* This enum is used to represent the current state of the camera. */
public enum CameraState {
    UnfocusedRoom1,         // Focus = 0
    FocusingCabinet,        // Focus = 1
    FocusingTableGroup1,    // Focus = 2
    FocusingTableGroup2,    // Focus = 3
    FocusingDesk1,          // Focus = 4
    FocusingDesk2,          // Focus = 5
    FocusingPeriodicTable,  // Focus = 6
    UnfocusedRoom2,         // Focus = 7
    FocusingBookPile1,      // Focus = 8
    FocusingGloves,         // Focus = 9
    FocusingBookPile3,      // Focus = 10
    FocusingSafe,           // Focus = 11
    FocusingSulfate,        // Focus = 12
    FocusingBookPile4       // Focus = 13
}
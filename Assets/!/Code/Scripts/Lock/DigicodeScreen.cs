using UnityEngine;
using TMPro;

// Class for the Digicode UI Screen.
public class DigicodeScreen : UnlockScreen<DigicodeInteractions> {
    public TextMeshProUGUI answer;

    private DigicodeInteractions digicode;

    /// <summary>
    /// Function to call as a constructor just after instantiation.
    /// </summary>
    /// <param name="DigicodeInteractions">Digicode linked to the Digicode Screen.</param>
    public override void Initialize(DigicodeInteractions digicode) {
        this.digicode = digicode;
        
        this.ResetTry();
    }

    /// <summary>
    /// Inputs a number, adding it to the answer string.
    /// </summary>
    /// <param name="number">Number to add.</param>
    public void InputNumber (int number) {
        if (this.answer.text == "FAUX") {
            this.ResetTry();
        }

        if (this.answer.text.Length < 6) {
            this.answer.text += number.ToString();
        }
    }

    /// <summary>
    /// Function that checks if the current try matches the code of the digicode.
    /// It either unlocks the door or displays an incorrect code message.
    /// </summary>
    public void ConfirmTry() {
        if (this.digicode.code == this.answer.text) {
            Debug.Log("Correct code");
            this.digicode.ConfirmTry(this.answer.text);
            this.CloseWindow();
        } else {
            this.answer.text = "FAUX";
        }
    }

    public void ResetTry() {
        this.answer.text = "";
    }
}
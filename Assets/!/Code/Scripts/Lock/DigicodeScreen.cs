using UnityEngine;
using TMPro;

// Class for the Digicode UI Screen.
public class DigicodeScreen : UnlockScreen<DigicodeInteractions> {
    public TextMeshProUGUI answer;

    private DigicodeInteractions digicode;

    private string blockedMessage;

    /// <summary>
    /// Function to call as a constructor just after instantiation.
    /// </summary>
    /// <param name="DigicodeInteractions">Digicode linked to the Digicode Screen.</param>
    public override void Initialize(DigicodeInteractions digicode) {
        base.Initialize(digicode);

        this.digicode = digicode;

        if (this.blockedMessage is null) {
            this.blockedMessage = "ALARM";
        }
        
        this.ResetTry();
    }

    /// <summary>
    /// Function to call as a constructor just after instantiation.
    /// </summary>
    /// <param name="DigicodeInteractions">Digicode linked to the Digicode Screen.</param>
    /// <param name="string">Message to display when the digicode is blocked.</param>
    public void Initialize(DigicodeInteractions digicode, string blockedMessage) {
        this.blockedMessage = blockedMessage;
        
        this.Initialize(digicode);
    }

    /// <summary>
    /// Inputs a number, adding it to the answer string.
    /// </summary>
    /// <param name="number">Number to add.</param>
    public void InputNumber (int number) {
        if (this.answer.text == "FAUX" || (!this.digicode.IsBlocked() && this.answer.text == this.blockedMessage)) {
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
        int tryCountWithError = this.digicode.GetTryCount();

        if (this.digicode.code != this.answer.text) {
            tryCountWithError++;
        }

        if (tryCountWithError >= this.digicode.maxNbTries) {
            this.answer.text = this.blockedMessage;
            this.digicode.BlockDigicode();
        } else if (this.digicode.code == this.answer.text) {
            Debug.Log("Correct code");
            this.digicode.ConfirmTry(this.answer.text);
            this.CloseWindow();
        } else {
            this.answer.text = "FAUX";
            this.digicode.IncrementTryCount();
        }
    }

    public void ResetTry() {
        this.answer.text = "";
    }
}
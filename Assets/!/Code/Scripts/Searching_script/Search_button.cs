using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using SearchObject;

public class Search_button : MonoBehaviour
{
    public GameObject button;
    public GameObject Popup_found;
    public GameObject TextPopup;
    private Search_object variab;
    private TextMeshProUGUI textMeshPro;

    public ItemObject QRCodeDeck;

    public void ToggleVisibilityButton(){
        button.SetActive(false);
    }

    public void ToggleVisibilityPopup(){
        if (Search_object.QR == true){
            if (Popup_found.activeSelf){
                Popup_found.SetActive(false);
                Search_object.QR = false;
                variab = GameObject.Find(Search_object.nameobj).GetComponent<Search_object>();
                variab.QRcode = false;
                }
            else{
                textMeshPro = TextPopup.GetComponent<TextMeshProUGUI>();
                textMeshPro.text = "Vous avez trouvé un morceau de QRcode !";
                variab = GameObject.Find(Search_object.nameobj).GetComponent<Search_object>();
                ItemObject qr = variab.item;
                if(qr is not null) {
                    // add QRCode deck to player inventory if no QR Code found
                    if(QRCodeDeck is not null && qr.inventory.Count() == 0) {
                        QRCodeDeck.inventory.AddItem(QRCodeDeck);
                    }
                    // add QRCode to QR Code deck
                    qr.inventory.AddItem(qr);

                }
                textMeshPro.fontSize = 20;
                Popup_found.SetActive(true);
            }
        }

        else if (Search_object.phone == true){
            if (Popup_found.activeSelf){
                Popup_found.SetActive(false);
                Search_object.phone = false;
                variab = GameObject.Find(Search_object.nameobj).GetComponent<Search_object>();
                variab.Phone = false;
                }
            else{
                textMeshPro = TextPopup.GetComponent<TextMeshProUGUI>();
                textMeshPro.text = "Vous avez trouvé un téléphone !";
                variab = GameObject.Find(Search_object.nameobj).GetComponent<Search_object>();
                if(variab.item is not null) {
                    variab.item.inventory.AddItem(variab.item);
                }
                textMeshPro.fontSize = 20;
                Popup_found.SetActive(true);
            }
        }

        else{
            if (Popup_found.activeSelf){
                Popup_found.SetActive(false);
            }
            else{
                textMeshPro = TextPopup.GetComponent<TextMeshProUGUI>();
                textMeshPro.text = "Il n'y a rien d'utile ici";
                textMeshPro.fontSize = 25;
                Popup_found.SetActive(true);
            }
        }
    }
}

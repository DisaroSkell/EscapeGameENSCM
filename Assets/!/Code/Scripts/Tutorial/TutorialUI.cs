using System.Collections.Generic;
using UnityEngine;
using TMPro;

/* Class to handle tutorial UI */
public class TutorialUI : MonoBehaviour {
    private int currentPage;
    private List<GameObject> pages;

    public GameObject pagesParent;

    public TextMeshProUGUI pageNumber;

    public void Start() {
        this.pages = Utils.GetAllChildren(this.pagesParent);
        this.SetCurrentPage(1);
    }

    public void SetCurrentPage(int page) {
        Utils.DisableAllChildren(this.pagesParent);
        this.currentPage = page;
        this.pageNumber.text = page.ToString();
        this.pages[page].SetActive(true);
    }

    /// <summary>
    /// If the page isn't the last, sets the page to the next value.
    /// </summary>
    public void Next() {
        if (this.currentPage < pages.Count - 1) {
            this.SetCurrentPage(this.currentPage + 1);
        }
    }

    /// <summary>
    /// If the page isn't the first, sets the page to the previous value.
    /// </summary>
    public void Previous() {
        if (this.currentPage > 1) {
            this.SetCurrentPage(this.currentPage - 1);
        }
    }

    /// <summary>
    /// Closes the UI.
    /// </summary>
    public void CloseUI() {
        this.gameObject.SetActive(false);
    }
}
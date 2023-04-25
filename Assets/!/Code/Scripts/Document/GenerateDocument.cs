using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using UnityEditor;

public class GenerateDocument : MonoBehaviour
{
    public GameObject imagePrefab;  // le prefab de l'objet Image à instancier
    public Transform parentPanel;  // le transform parent du panel qui contiendra les images
    public Sprite oneImageSpriteOfThePDF; // one image sprite of the pdf
    public float imageGap = 10f;   // l'écart voulu entre chaque image en pixels
    public float sideGap = 20f;    // l'écart voulu sur les côtés en pixels

    public float topAndBottomGap = 10f;


    // fonction pour charger les images à partir d'un dossier
    List<Texture2D> LoadImagesFromFolder(string folderPath)
    {
        List<Texture2D> images = new List<Texture2D>();

        // vérifie si le dossier existe
        if (!Directory.Exists(folderPath))
        {
            Debug.LogError("Le dossier d'images n'existe pas : " + folderPath);
            return images;
        }

        // charge toutes les images dans le dossier
        string[] imagePaths = Directory.GetFiles(folderPath, "*.png");

        foreach (string imagePath in imagePaths)
        {
            byte[] imageData = File.ReadAllBytes(imagePath);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(imageData);
            images.Add(texture);
        }

        Debug.Log(images.Count);

        return images;
    }

    // fonction pour afficher les images dans le panel
    void DisplayImagesInPanel(List<Texture2D> images)
    {
        /*
        float panelWidth = this.parentPanel.GetComponent<RectTransform>().rect.width;
        float imageWidth = panelWidth - (2 * sideGap);
        float imageHeight = imageWidth / images[0].width * images[0].height;
        */

        RectTransform contentRectTransform = this.GetComponent<RectTransform>();
        float totalImageHeight = 0f;
        float firstImageHeight = 0f;

        if (images.Count > 0)
        {
            // calculate height of first image
            float firstImageAspectRatio = (float)images[0].width / (float)images[0].height;
            firstImageHeight = (contentRectTransform.rect.width - sideGap * 2) / firstImageAspectRatio;
        }

        // Vector3 imagePosition = new Vector3(0, 0, 0);
        int i = 0;
        foreach (Texture2D image in images)
        {
            float aspectRatio = (float)images[i].width / (float)images[i].height;
            float imageWidth = contentRectTransform.rect.width - sideGap * 2;
            float imageHeight = imageWidth / aspectRatio;
            
            GameObject imageObject = Instantiate(imagePrefab, this.transform);
            Image imageComponent = imageObject.GetComponent<Image>();
            imageComponent.sprite = Sprite.Create(image, new Rect(0, 0, image.width, image.height), new Vector2(0.5f, 0.5f));
            imageComponent.rectTransform.sizeDelta = new Vector2(imageWidth, imageHeight);
            RectTransform imageRT = imageObject.GetComponent<RectTransform>();
            imageRT.pivot = new Vector2(imageRT.pivot.x, 1f);
            //imageRT.anchorMax = new Vector2(imageRT.anchorMax.x, 1f);


            // imageObject.transform.localPosition = imagePosition;
            /**/
            if (i == 0) {
                // adjust position of first image to align with top of Content
                imageComponent.rectTransform.localPosition = new Vector3(0f, -topAndBottomGap, 0f);
            }
            else
            {
                imageComponent.rectTransform.localPosition = new Vector3(0f, -totalImageHeight, 0f);
            }
            
            //imageComponent.rectTransform.localPosition = new Vector3(0f, -totalImageHeight, 0f);
            i++;
            totalImageHeight += imageHeight + imageGap;
            // imagePosition.y -= imageHeight + imageGap;
        }
    }

    void AdjustContentSize(List<Texture2D> images) {
        RectTransform contentRectTransform = this.GetComponent<RectTransform>();
        float totalImageHeight = 0f;

        foreach (Texture2D image in images)
        {
            float aspectRatio = (float)image.width / (float)image.height;
            float imageHeight = (contentRectTransform.rect.width - 2*(float) sideGap) / aspectRatio;
            totalImageHeight += imageHeight + imageGap;
        }

        totalImageHeight -= imageGap; // remove last gap
        totalImageHeight += 2*topAndBottomGap;

        contentRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, totalImageHeight);
    }

    // exemple d'utilisation : chargement des images depuis un dossier et affichage dans le panel
    public void Display()
    {
        for (int i = 0; i < this.transform.childCount; i++) {
            // Delete each child
            Destroy(this.transform.GetChild(i).gameObject);
        }
        string assetPath = AssetDatabase.GetAssetPath(oneImageSpriteOfThePDF);
        string path = System.IO.Path.GetDirectoryName(assetPath);
        List<Texture2D> images = LoadImagesFromFolder(path);
        AdjustContentSize(images);
        DisplayImagesInPanel(images);
    }
}

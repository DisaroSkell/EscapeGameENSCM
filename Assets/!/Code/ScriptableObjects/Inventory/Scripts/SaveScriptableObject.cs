using UnityEditor;
using UnityEngine;
using System.IO;

[CustomEditor(typeof(Inventory))]
public class MyScriptableObjectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Inventory myScriptableObject = (Inventory)target;

        string fileName = GetFileName(myScriptableObject) + ".json";
        // Ajoute un bouton pour sauvegarder l'état de la liste
        if (GUILayout.Button("Sauvegarder la liste"))
        {
            SaveList(myScriptableObject.Container, fileName);
        }
        // Ajoute un bouton pour sauvegarder l'état de la liste
        if (GUILayout.Button("Charger la liste"))
        {
            LoadList(myScriptableObject.Container, fileName);
        }
    }

    private void SaveList(ItemObject[] array, string fileName)
    {
        // Convertit la liste en format JSON
        string json = JsonUtility.ToJson(new MyObjectList(array), true);

        // Ecrit le JSON dans un fichier
        string filePath = Application.persistentDataPath + "/" + fileName;
        File.WriteAllText(filePath, json);

        Debug.Log("List saved at : " + filePath);
    }

    # nullable disable
    private string _filePath;

    public void LoadList(ItemObject[] array, string fileName) {
        // Charge la liste d'objets depuis le fichier JSON
        _filePath = Application.persistentDataPath + "/" + fileName;
        MyObjectList a = new MyObjectList(array);
        if (File.Exists(_filePath))
        {
            string json = File.ReadAllText(_filePath);
            JsonUtility.FromJsonOverwrite(json, a);
            array = a.array;
            Debug.Log("List Load from : " + _filePath);
        }
    }

    private string GetFileName(Inventory myScriptableObject) {
        string assetPath = AssetDatabase.GetAssetPath(myScriptableObject);
        return System.IO.Path.GetFileNameWithoutExtension(assetPath);
    }

    private class MyObjectList
    {
        public ItemObject[] array;

        public MyObjectList(ItemObject[] array)
        {
            this.array = array;
        }
    }
}

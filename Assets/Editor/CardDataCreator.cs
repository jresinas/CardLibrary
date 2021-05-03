using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CardDataCreator {
    static string sourcePath = "Assets/Core/Cards/CardData/CardData.prefab";
    static string destinyPath = "Assets/Resources/Cards/CardData/";
    static string extension = ".prefab";
    static string templatePath = "Assets/Resources/Cards/CardTemplate/";

    // Creates a new menu item 'Examples > Create Prefab' in the main menu.
    [MenuItem("Assets/Create/Card Data", false, 51)]
    static void OpenOptions() {
        new PrefabOptions(templatePath);
    }

    // Disable the menu item if no selection is in place.
    [MenuItem("Assets/Create/Card Data", true)]
    static bool ValidateCreatePrefab() {
        return true;
    }


    public static void CreatePrefab(string name, GameObject template) {
        GameObject originalPrefab = (GameObject)AssetDatabase.LoadAssetAtPath(sourcePath, typeof(GameObject));
        GameObject objSource = PrefabUtility.InstantiatePrefab(originalPrefab) as GameObject;


        int i = 0;
        string newName = name;
        while (AssetDatabase.LoadAssetAtPath(destinyPath + newName + extension, typeof(GameObject)) != null) {
            i++;
            newName = name + i;
        }


        GameObject prefabVariant = PrefabUtility.SaveAsPrefabAsset(objSource, destinyPath + newName + extension);
        CardData cardData = prefabVariant.GetComponent<CardData>();
        cardData.name = name;
        cardData.cardTemplate = template;

        GameObject.DestroyImmediate(objSource);
    }
}


public class PrefabOptions : EditorWindow {
    string path;
    int index = 0;
    string name;

    public PrefabOptions(string path) {
        this.path = path;
        Init();
    }

    static void Init() {
        // Get existing open window or if none, make a new one:
        PrefabOptions window = (PrefabOptions)EditorWindow.GetWindow(typeof(PrefabOptions));
        window.Show();
    }

    void OnGUI() {
        GUILayout.Label("New Card Data", EditorStyles.boldLabel);
        name = EditorGUILayout.TextField("Name", name);

        /*
        groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
        myBool = EditorGUILayout.Toggle("Toggle", myBool);
        myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
        EditorGUILayout.EndToggleGroup();
        */

        GameObject[] listPrefabs = EditorUtils.GetOptions<GameObject>(path).ToArray(); //GetPrefabs(path).ToArray();
        string[] options = new string[listPrefabs.Length];
        for (int i = 0; i < listPrefabs.Length; i++) {
            if (listPrefabs[i] != null) options[i] = listPrefabs[i].name.ToString();
        }

        index = EditorGUILayout.Popup(index, options);



        if (GUILayout.Button("Create")) {
            CardDataCreator.CreatePrefab(name, listPrefabs[index]);
            Close();
        }
    }
}
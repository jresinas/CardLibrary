using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CardTemplateCreator {
    static string sourcePath = "Assets/Core/Cards/CardTemplate/CardTemplate.prefab";
    static string destinyPath = "Assets/Resources/Cards/CardTemplate/";
    static string name = "CardTemplate";
    static string extension = ".prefab";

    // Creates a new menu item 'Examples > Create Prefab' in the main menu.
    [MenuItem("Assets/Create/Card Template", false, 52)]
    static void CreatePrefab() {
        GameObject originalPrefab = (GameObject)AssetDatabase.LoadAssetAtPath(sourcePath, typeof(GameObject));
        GameObject objSource = PrefabUtility.InstantiatePrefab(originalPrefab) as GameObject;

        int i = 0;
        string newName = name;
        while (AssetDatabase.LoadAssetAtPath(destinyPath + newName + extension, typeof(GameObject)) != null) {
            i++;
            newName = name + i;
        }

        GameObject prefabVariant = PrefabUtility.SaveAsPrefabAsset(objSource, destinyPath + newName + extension);
        GameObject.DestroyImmediate(objSource);
    }

    // Disable the menu item if no selection is in place.
    [MenuItem("Assets/Create/Card Template", true)]
    static bool ValidateCreatePrefab() {
        return AssetDatabase.LoadAssetAtPath(sourcePath, typeof(GameObject));
    }
}
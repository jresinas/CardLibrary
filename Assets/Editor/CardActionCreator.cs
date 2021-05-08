using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CardActionCreator {
    static string sourcePath = "Assets/Core/Cards/CardData/CardData.prefab";
    static string destinyPath = "Assets/Resources/Cards/CardData/";
    static string extension = ".prefab";
    static string effectsPath = "Assets/Resources/Cards/CardEffects/";

    // Creates a new menu item 'Examples > Create Prefab' in the main menu.
    [MenuItem("Assets/Create/Card Action", false, 51)]
    static void OpenOptions() {
        new CardActionOptions(effectsPath);
    }

    // Disable the menu item if no selection is in place.
    [MenuItem("Assets/Create/Card Action", true)]
    static bool ValidateCreatePrefab() {
        return Selection.activeGameObject != null && Selection.activeGameObject.GetComponent<CardData>();
    }


    public static void CreatePrefab(string name, MonoBehaviour template) {
        /*
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
        */
    }
}

public class CardActionOptions : EditorWindow {
    string path;
    int index = 0;
    string name;

    public CardActionOptions(string path) {
        this.path = path;
        Init();
    }

    static void Init() {
        // Get existing open window or if none, make a new one:
        CardActionOptions window = (CardActionOptions)EditorWindow.GetWindow(typeof(CardActionOptions));
        window.Show();
    }

    public string[] Strings = { };

    int flags = 0;

    void OnGUI() {
        GUILayout.Label("New Card Action", EditorStyles.boldLabel);
        name = EditorGUILayout.TextField("Name", name);

        /*
        groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
        myBool = EditorGUILayout.Toggle("Toggle", myBool);
        myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
        EditorGUILayout.EndToggleGroup();
        */

        //CardEffect[] listOptions = EditorUtils.GetOptions<CardEffect>(path).ToArray();
        MonoBehaviour[] listOptions = EditorUtils.GetOptions<MonoBehaviour>(path).ToArray();
        Debug.Log(listOptions[0]);
        string[] options = new string[listOptions.Length];
        for (int i = 0; i < listOptions.Length; i++) {
            if (listOptions[i] != null) options[i] = listOptions[i].name.ToString();
        }

        index = EditorGUILayout.Popup(index, options);

        /*
        Debug.Log(options);
        Debug.Log(options[0]);


        flags = EditorGUILayout.MaskField("Effects", flags, options);
        List<string> selectedOptions = new List<string>();
        for (int i = 0; i < options.Length; i++) {
            if ((flags & (1 << i)) == (1 << i)) selectedOptions.Add(options[i]);
        }
        */
        /*
        if (GUILayout.Button("Print Options")) {
            foreach (string o in selectedOptions) Debug.Log(o);
        }
        */



        ScriptableObject target = this;
        SerializedObject so = new SerializedObject(target);
        SerializedProperty stringsProperty = so.FindProperty("Strings");

        EditorGUILayout.PropertyField(stringsProperty, true); // True means show children
        so.ApplyModifiedProperties(); // Remember to apply modified properties








        if (GUILayout.Button("Create")) {
            CardActionCreator.CreatePrefab(name, listOptions[index]);
            Close();
        }
    }
}
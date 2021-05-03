using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EditorUtils {
    public static List<T> GetOptions<T>(string path) {
        List<T> options = new List<T>();
        string[] filePaths = System.IO.Directory.GetFiles(path);
        int countFound = 0;
        Debug.Log(filePaths.Length);
        Debug.Log(filePaths[0]);
        Debug.Log(filePaths[1]);
        Debug.Log(filePaths[2]);
        if (filePaths != null && filePaths.Length > 0) {
            for (int i = 0; i < filePaths.Length; i++) {
                UnityEngine.Object obj = UnityEditor.AssetDatabase.LoadAssetAtPath(filePaths[i], typeof(T));
                Debug.Log(obj);
                if (obj is T asset) {
                    Debug.Log("is asset");
                    countFound++;
                    if (!options.Contains(asset)) {
                        options.Add(asset);
                    }
                }
            }
        }

        return options;
    }
}

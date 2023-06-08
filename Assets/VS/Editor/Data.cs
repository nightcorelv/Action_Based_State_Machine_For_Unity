using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Core.Data
{
    public class Data
    {
        [MenuItem("Assets/Visual Scripting/ConditionDatabase")]
        public static void CreateConditionList()
        {
            CreateScriptableObject("ConditionDatabase");
        }

        [MenuItem("Assets/Visual Scripting/ActionDatabase")]
        public static void ActionDatabase()
        {
            CreateScriptableObject("ActionDatabase");
        }

        private static void CreateScriptableObject(string className)
        {
            //path
            string path = AssetDatabase.GetAssetPath(UnityEditor.Selection.activeInstanceID);
            if (path.Contains("."))
            {
                path = path.Remove(path.LastIndexOf('/'));
            }
            ScriptableObject asset = ScriptableObject.CreateInstance(className);
            path = AssetDatabase.GenerateUniqueAssetPath(path + "/" + className + ".asset");

            //create
            AssetDatabase.CreateAsset(asset, path);
            AssetDatabase.SaveAssets();

            //fix
            EditorUtility.FocusProjectWindow();
            UnityEditor.Selection.activeObject = asset;
        }
    }
}
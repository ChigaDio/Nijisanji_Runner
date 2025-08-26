using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class MaterialReplacer : EditorWindow
{
    private string materialFolder = "Assets/Materials"; // デフォルトパス
    private bool hasSkinnedMeshRenderer = false;

    [MenuItem("Tools/Replace Materials by Name")]
    static void ShowWindow()
    {
        GetWindow<MaterialReplacer>("Material Replacer");
    }

    void OnSelectionChange()
    {
        // 選択状態が変わったら更新
        Repaint();
    }

    void OnGUI()
    {
        GUILayout.Label("Material Replace Settings", EditorStyles.boldLabel);

        EditorGUILayout.BeginHorizontal();
        materialFolder = EditorGUILayout.TextField("Material Folder", materialFolder);
        if (GUILayout.Button("Select Folder", GUILayout.Width(100)))
        {
            string selectedPath = EditorUtility.OpenFolderPanel("Select Material Folder", Application.dataPath, "");
            if (!string.IsNullOrEmpty(selectedPath))
            {
                // プロジェクトパスからの相対パスに変換
                if (selectedPath.StartsWith(Application.dataPath))
                {
                    materialFolder = "Assets" + selectedPath.Substring(Application.dataPath.Length);
                }
                else
                {
                    EditorUtility.DisplayDialog("Error", "プロジェクト内のフォルダを選択してください。", "OK");
                }
            }
        }
        EditorGUILayout.EndHorizontal();

        // 現在の選択にSkinnedMeshRendererがあるかチェック
        hasSkinnedMeshRenderer = CheckSelectionHasSkinnedMeshRenderer();

        // 視覚的な状態表示
        EditorGUILayout.Space();
        if (Selection.gameObjects.Length == 0)
        {
            EditorGUILayout.HelpBox("オブジェクトが選択されていません。", MessageType.Info);
        }
        else if (!hasSkinnedMeshRenderer)
        {
            EditorGUILayout.HelpBox("選択されたオブジェクトに SkinnedMeshRenderer がありません。", MessageType.Warning);
        }
        else
        {
            EditorGUILayout.HelpBox("SkinnedMeshRenderer が見つかりました。置き換え可能です。", MessageType.Info);
        }

        EditorGUI.BeginDisabledGroup(!hasSkinnedMeshRenderer);
        if (GUILayout.Button("Replace Materials on Selected"))
        {
            ReplaceMaterials();
        }
        EditorGUI.EndDisabledGroup();
    }

    bool CheckSelectionHasSkinnedMeshRenderer()
    {
        foreach (GameObject obj in Selection.gameObjects)
        {
            if (obj.GetComponent<SkinnedMeshRenderer>() != null)
                return true;
        }
        return false;
    }

    void ReplaceMaterials()
    {
        if (Selection.gameObjects.Length == 0) return;

        string[] materialGuids = AssetDatabase.FindAssets("t:Material", new[] { materialFolder });
        Dictionary<string, Material> materialDict = new Dictionary<string, Material>();

        foreach (string guid in materialGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Material mat = AssetDatabase.LoadAssetAtPath<Material>(path);
            if (mat != null && !materialDict.ContainsKey(mat.name))
            {
                materialDict.Add(mat.name, mat);
            }
        }

        Undo.RecordObjects(Selection.gameObjects, "Replace Materials");

        foreach (GameObject obj in Selection.gameObjects)
        {
            SkinnedMeshRenderer renderer = obj.GetComponent<SkinnedMeshRenderer>();
            if (renderer == null) continue;

            Material[] mats = renderer.sharedMaterials;
            for (int i = 0; i < mats.Length; i++)
            {
                if (mats[i] != null && materialDict.TryGetValue(mats[i].name, out Material newMat))
                {
                    mats[i] = newMat;
                }
            }
            renderer.sharedMaterials = mats;
        }

        Debug.Log("Material置き換え完了");
    }
}

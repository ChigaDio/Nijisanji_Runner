using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class MaterialReplacer : EditorWindow
{
    private string materialFolder = "Assets/Materials"; // �f�t�H���g�p�X
    private bool hasSkinnedMeshRenderer = false;

    [MenuItem("Tools/Replace Materials by Name")]
    static void ShowWindow()
    {
        GetWindow<MaterialReplacer>("Material Replacer");
    }

    void OnSelectionChange()
    {
        // �I����Ԃ��ς������X�V
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
                // �v���W�F�N�g�p�X����̑��΃p�X�ɕϊ�
                if (selectedPath.StartsWith(Application.dataPath))
                {
                    materialFolder = "Assets" + selectedPath.Substring(Application.dataPath.Length);
                }
                else
                {
                    EditorUtility.DisplayDialog("Error", "�v���W�F�N�g���̃t�H���_��I�����Ă��������B", "OK");
                }
            }
        }
        EditorGUILayout.EndHorizontal();

        // ���݂̑I����SkinnedMeshRenderer�����邩�`�F�b�N
        hasSkinnedMeshRenderer = CheckSelectionHasSkinnedMeshRenderer();

        // ���o�I�ȏ�ԕ\��
        EditorGUILayout.Space();
        if (Selection.gameObjects.Length == 0)
        {
            EditorGUILayout.HelpBox("�I�u�W�F�N�g���I������Ă��܂���B", MessageType.Info);
        }
        else if (!hasSkinnedMeshRenderer)
        {
            EditorGUILayout.HelpBox("�I�����ꂽ�I�u�W�F�N�g�� SkinnedMeshRenderer ������܂���B", MessageType.Warning);
        }
        else
        {
            EditorGUILayout.HelpBox("SkinnedMeshRenderer ��������܂����B�u�������\�ł��B", MessageType.Info);
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

        Debug.Log("Material�u����������");
    }
}

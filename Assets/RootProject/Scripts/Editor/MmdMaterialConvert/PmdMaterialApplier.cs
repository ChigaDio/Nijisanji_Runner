using UnityEngine;
using UnityEditor;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System;

public class pmxMaterialApplier : EditorWindow
{
    private string exePath = "";            // get_materials.exe
    private string pmxPath = "";
    private string materialFolder = "Assets/Materials";
    private string textureFolder = "Assets/Textures";

    [MenuItem("Tools/Apply pmx Materials (EXE)")]
    static void ShowWindow()
    {
        GetWindow<pmxMaterialApplier>("pmx Material Applier");
    }

    void OnEnable()
    {
        // EditorWindow 立ち上げ時に get_materials.exe を自動検索
        string[] exeGuids = AssetDatabase.FindAssets("get_materials t:DefaultAsset");
        foreach (string guid in exeGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            if (Path.GetFileName(path) == "get_materials.exe")
            {
                exePath = path;
                UnityEngine.Debug.Log($"Automatically found get_materials.exe at: {exePath}");
                break;
            }
        }
        if (string.IsNullOrEmpty(exePath))
        {
            UnityEngine.Debug.LogWarning("get_materials.exe not found in project.");
        }
    }

    void OnGUI()
    {
        GUILayout.Label("pmx Material Applier", EditorStyles.boldLabel);

        // EXE パス
        EditorGUILayout.BeginHorizontal();
        exePath = EditorGUILayout.TextField("get_materials.exe", exePath);
        if (GUILayout.Button("Select", GUILayout.Width(80)))
        {
            string selectedPath = EditorUtility.OpenFilePanel("Select EXE", Application.dataPath, "exe");
            if (!string.IsNullOrEmpty(selectedPath) && selectedPath.StartsWith(Application.dataPath))
            {
                exePath = "Assets" + selectedPath.Substring(Application.dataPath.Length);
            }
        }
        EditorGUILayout.EndHorizontal();

        // pmx パス
        EditorGUILayout.BeginHorizontal();
        string newPmxPath = EditorGUILayout.TextField("pmx File", pmxPath);
        if (newPmxPath != pmxPath)
        {
            pmxPath = newPmxPath;
            if (!string.IsNullOrEmpty(pmxPath))
            {
                // pmxPath が指定されたら Material と Texture フォルダを自動検索
                string pmxDir = Path.GetDirectoryName(pmxPath);
                // Material フォルダの自動検索
                materialFolder = "";
                string materialsPath = Path.Combine(pmxDir, "Materials");
                if (Directory.Exists(Path.GetFullPath(materialsPath)))
                {
                    materialFolder = materialsPath;
                }
                else
                {
                    materialsPath = Path.Combine(pmxDir, "materials");
                    if (Directory.Exists(Path.GetFullPath(materialsPath)))
                    {
                        materialFolder = materialsPath;
                    }
                }
                if (!string.IsNullOrEmpty(materialFolder))
                {
                    UnityEngine.Debug.Log($"Automatically set Material folder to: {materialFolder}");
                }
                else
                {
                    UnityEngine.Debug.LogWarning($"Material folder not found in {pmxDir}");
                    materialFolder = "Assets/Materials";
                }

                // Texture フォルダの自動検索
                textureFolder = "";
                string texturesPath = Path.Combine(pmxDir, "Textures");
                if (Directory.Exists(Path.GetFullPath(texturesPath)))
                {
                    textureFolder = texturesPath;
                }
                else
                {
                    texturesPath = Path.Combine(pmxDir, "textures");
                    if (Directory.Exists(Path.GetFullPath(texturesPath)))
                    {
                        textureFolder = texturesPath;
                    }
                    else
                    {
                        texturesPath = Path.Combine(pmxDir, "tex");
                        if (Directory.Exists(Path.GetFullPath(texturesPath)))
                        {
                            textureFolder = texturesPath;
                        }
                    }
                }
                if (!string.IsNullOrEmpty(textureFolder))
                {
                    UnityEngine.Debug.Log($"Automatically set Texture folder to: {textureFolder}");
                }
                else
                {
                    UnityEngine.Debug.LogWarning($"Texture folder not found in {pmxDir}");
                    textureFolder = "Assets/Textures";
                }
            }
        }
        if (GUILayout.Button("Select", GUILayout.Width(80)))
        {
            string selectedPath = EditorUtility.OpenFilePanel("Select pmx File", Application.dataPath, "pmx");
            if (!string.IsNullOrEmpty(selectedPath) && selectedPath.StartsWith(Application.dataPath))
            {
                pmxPath = "Assets" + selectedPath.Substring(Application.dataPath.Length);
            }
        }
        EditorGUILayout.EndHorizontal();

        // Material フォルダ
        EditorGUILayout.BeginHorizontal();
        materialFolder = EditorGUILayout.TextField("Material Folder", materialFolder);
        if (GUILayout.Button("Select", GUILayout.Width(80)))
        {
            string selectedPath = EditorUtility.OpenFolderPanel("Select Material Folder", Application.dataPath, "");
            if (!string.IsNullOrEmpty(selectedPath) && selectedPath.StartsWith(Application.dataPath))
            {
                materialFolder = "Assets" + selectedPath.Substring(Application.dataPath.Length);
            }
        }
        EditorGUILayout.EndHorizontal();

        // Texture フォルダ
        EditorGUILayout.BeginHorizontal();
        textureFolder = EditorGUILayout.TextField("Texture Folder", textureFolder);
        if (GUILayout.Button("Select", GUILayout.Width(80)))
        {
            string selectedPath = EditorUtility.OpenFolderPanel("Select Texture Folder", Application.dataPath, "");
            if (!string.IsNullOrEmpty(selectedPath) && selectedPath.StartsWith(Application.dataPath))
            {
                textureFolder = "Assets" + selectedPath.Substring(Application.dataPath.Length);
            }
        }
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Apply Materials"))
        {
            ApplyMaterials();
        }
    }

    void ApplyMaterials()
    {
        if (string.IsNullOrEmpty(exePath))
        {
            UnityEngine.Debug.LogError("get_materials.exe path is not set.");
            return;
        }

        if (string.IsNullOrEmpty(pmxPath))
        {
            UnityEngine.Debug.LogError("pmx file path is not set.");
            return;
        }

        if (string.IsNullOrEmpty(materialFolder))
        {
            UnityEngine.Debug.LogError("Material folder path is not set.");
            return;
        }

        if (string.IsNullOrEmpty(textureFolder))
        {
            UnityEngine.Debug.LogError("Texture folder path is not set.");
            return;
        }

        string absExe = Path.GetFullPath(exePath);
        string abspmx = Path.GetFullPath(pmxPath);

        string jsonData = RunExternalProcess(absExe, abspmx);
        if (string.IsNullOrEmpty(jsonData))
        {
            UnityEngine.Debug.LogError("EXE の実行に失敗しました。");
            return;
        }

        List<MaterialInfo> materials = JsonUtilityWrapper.FromJsonList<MaterialInfo>(jsonData);
        if (materials == null)
        {
            UnityEngine.Debug.LogError("JSON のパースに失敗しました。");
            return;
        }

        UnityEngine.Debug.Log($"Parsed {materials.Count} materials");
        foreach (var mat in materials)
        {
            UnityEngine.Debug.Log($"Material: {mat.name}, Texture: {mat.texture}");
        }

        string[] materialGuids = AssetDatabase.FindAssets("t:Material", new[] { materialFolder });
        string[] textureGuids = AssetDatabase.FindAssets("t:Texture2D", new[] { textureFolder });

        UnityEngine.Debug.Log($"Found {textureGuids.Length} textures in {textureFolder}");
        var textureDict = new Dictionary<string, Texture2D>();
        foreach (string guid in textureGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;
            if (importer != null)
            {
                importer.textureType = TextureImporterType.Default;
                importer.sRGBTexture = true;
                importer.SaveAndReimport();
            }
            Texture2D tex = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
            if (tex != null && !textureDict.ContainsKey(tex.name.ToLower()))
            {
                textureDict.Add(tex.name.ToLower(), tex);
                UnityEngine.Debug.Log($"Texture added: {tex.name}, Path: {path}");
            }
        }

        foreach (string matGuid in materialGuids)
        {
            string matPath = AssetDatabase.GUIDToAssetPath(matGuid);
            Material mat = AssetDatabase.LoadAssetAtPath<Material>(matPath);
            if (mat == null) continue;

            UnityEngine.Debug.Log($"Processing material: {mat.name}, Shader: {mat.shader.name}");
            MaterialInfo info = materials.Find(m => m.name.ToLower() == mat.name.ToLower());
            if (info != null && !string.IsNullOrEmpty(info.texture))
            {
                string texName = Path.GetFileNameWithoutExtension(info.texture).ToLower();
                if (textureDict.TryGetValue(texName, out Texture2D tex))
                {
                    mat.shader = Shader.Find("Toon"); // シェーダー名を明示
                    mat.SetTexture("_BaseMap", tex);
                    mat.SetTexture("_BaseColorMap", tex); // HDRP用
                    mat.SetTexture("_MainTex", tex); // Legacy用
                    mat.SetFloat("_BaseColorVisible", 1f); // ベースカラーを表示
                    mat.SetFloat("_BaseColorOverridden", 0f); // 上書きを無効化
                    mat.SetColor("_BaseColor", Color.white); // ベースカラーを白色に
                    mat.SetFloat("_Use_BaseAs1st", 1f); // _Use_BaseAs1st を 1 に設定
                    mat.SetFloat("_Use_1stAs2nd", 1f); // _Use_1stAs2nd を 0 に設定
                    EditorUtility.SetDirty(mat);
                    AssetDatabase.SaveAssetIfDirty(mat);
                    UnityEngine.Debug.Log($"Applied texture {texName} to material {mat.name}, BaseMap: {mat.GetTexture("_BaseMap")?.name}");
                }
                else
                {
                    UnityEngine.Debug.LogWarning($"Texture {texName} not found for material {mat.name}");
                }
            }
            else
            {
                UnityEngine.Debug.LogWarning($"MaterialInfo not found for material {mat.name}");
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        UnityEngine.Debug.Log("マテリアル適用完了");
    }

    string RunExternalProcess(string exePath, string arg)
    {
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = exePath,
            Arguments = $"\"{arg}\"",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            StandardOutputEncoding = Encoding.UTF8,
            CreateNoWindow = true
        };

        try
        {
            using (Process process = Process.Start(startInfo))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string output = reader.ReadToEnd();
                    UnityEngine.Debug.Log($"Raw EXE output: {output}");
                    int startIndex = output.LastIndexOf('[');
                    if (startIndex >= 0)
                    {
                        output = output.Substring(startIndex);
                        UnityEngine.Debug.Log($"Parsed JSON: {output}");
                    }
                    process.WaitForExit();
                    return output;
                }
            }
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogError("EXE 実行エラー: " + e.Message);
            return null;
        }
    }

    [Serializable]
    public class MaterialInfo
    {
        public string name;
        public string texture;
    }
}

public static class JsonUtilityWrapper
{
    [Serializable]
    private class Wrapper<T> { public List<T> Items; }

    public static List<T> FromJsonList<T>(string json)
    {
        int endIndex = json.LastIndexOf(']');
        int startIndex = json.IndexOf('[');
        if (startIndex < 0 || endIndex < 0) return null;

        string cleanJson = json.Substring(startIndex, endIndex - startIndex + 1);
        string newJson = "{\"Items\":" + cleanJson + "}";
        var wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
        return wrapper?.Items;
    }
}

//        string newJson = "{\"Items\":" + cleanJson + "}";
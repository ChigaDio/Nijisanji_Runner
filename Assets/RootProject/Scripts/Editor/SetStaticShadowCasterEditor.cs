using UnityEngine;


using UnityEditor;

public static class SetStaticShadowCasterEditor
{
    [MenuItem("Tools/Set Static Shadow Caster For Selected")]
    static void SetStaticShadowCaster()
    {
        foreach (GameObject obj in Selection.gameObjects)
        {
            MeshRenderer[] renderers = obj.GetComponentsInChildren<MeshRenderer>();

            foreach (MeshRenderer renderer in renderers)
            {
                renderer.staticShadowCaster = true;
                GameObject go = renderer.gameObject;

                // 現在の StaticEditorFlags を取得
                StaticEditorFlags flags = GameObjectUtility.GetStaticEditorFlags(go);

                // Static Shadow Caster のフラグを設定
                flags |= StaticEditorFlags.OccluderStatic; // Shadow Caster 用
                flags |= StaticEditorFlags.ContributeGI;   // 必要なら GI にも反映

                // 変更を反映
                GameObjectUtility.SetStaticEditorFlags(go, flags);

            }
        }
    }
}


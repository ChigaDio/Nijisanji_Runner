import os
from mmd_scripting.core.nuthouse01_vmd_parser import read_vmd
import math
from collections import defaultdict

# Unity Humanoidボーンへのマッピング（必須＋オプション）
bone_mapping = {
    # 必須ボーン
    "センター": "Hips",
    "下半身": "Spine",
    "上半身": "Chest",
    "上半身2": "UpperChest",
    "首": "Neck",
    "頭": "Head",
    "左腕": "LeftUpperArm",
    "左ひじ": "LeftLowerArm",
    "左手首": "LeftHand",
    "右腕": "RightUpperArm",
    "右ひじ": "RightLowerArm",
    "右手首": "RightHand",
    "左足": "LeftUpperLeg",
    "左ひざ": "LeftLowerLeg",
    "左足首": "LeftFoot",
    "右足": "RightUpperLeg",
    "右ひざ": "RightLowerLeg",
    "右足首": "RightFoot",
    "左肩": "LeftShoulder",
    "右肩": "RightShoulder",
    "左つま先": "LeftToes",
    "右つま先": "RightToes",
    "左目": "LeftEye",
    "右目": "RightEye",
    # オプションの指ボーン
    "左親指0": "LeftThumbProximal",
    "左親指1": "LeftThumbIntermediate",
    "左親指2": "LeftThumbDistal",
    "右親指0": "RightThumbProximal",
    "右親指1": "RightThumbIntermediate",
    "右親指2": "RightThumbDistal",
    "左人指1": "LeftIndexProximal",
    "左人指2": "LeftIndexIntermediate",
    "左人指3": "LeftIndexDistal",
    "右人指1": "RightIndexProximal",
    "右人指2": "RightIndexIntermediate",
    "右人指3": "RightIndexDistal",
    "左中指1": "LeftMiddleProximal",
    "左中指2": "LeftMiddleIntermediate",
    "左中指3": "LeftMiddleDistal",
    "右中指1": "RightMiddleProximal",
    "右中指2": "RightMiddleIntermediate",
    "右中指3": "RightMiddleDistal"
}

# VMD補間データをUnityのinSlope/outSlopeに変換
def get_slope_from_interpolation(interp, delta_time, delta_value):
    if not interp or delta_time == 0:
        return 0, 0
    # interp: [Ax, Ay, Bx, By] (0-127スケール)
    ax, ay, bx, by = [x / 127.0 for x in interp]
    # Unityのslopeは、時間と値の変化に対する傾き
    in_slope = (ay * delta_value) / (ax * delta_time) if ax != 0 else 0
    out_slope = ((1 - by) * delta_value) / ((1 - bx) * delta_time) if bx != 1 else 0
    return in_slope, out_slope

# FloatCurveを書き出す関数（補間データ対応）
def write_float_curve(f, path, attribute, keys, slopes=None):
    f.write("  - curve:\n")
    f.write("      serializedVersion: 2\n")
    f.write("      m_Curve:\n")
    for i, (time, value) in enumerate(keys):
        in_slope = out_slope = 0
        if slopes and i < len(slopes):
            in_slope, out_slope = slopes[i]
        f.write("      - serializedVersion: 3\n")
        f.write(f"        time: {time}\n")
        f.write(f"        value: {value}\n")
        f.write(f"        inSlope: {in_slope}\n")
        f.write(f"        outSlope: {out_slope}\n")
        f.write("        tangentMode: 136\n")
        f.write("        weightedMode: 0\n")
        f.write("        inWeight: 0.33333334\n")
        f.write("        outWeight: 0.33333334\n")
    f.write("      m_PreInfinity: 2\n")
    f.write("      m_PostInfinity: 2\n")
    f.write("      m_RotationOrder: 4\n")
    f.write(f"    attribute: {attribute}\n")
    f.write(f"    path: {path}\n")
    f.write(f"    classID: {4 if 'localPosition' in attribute or 'localEulerAngles' in attribute else 137}\n")
    f.write("    script: {{fileID: 0}}\n")

# VMDをUnity AnimationClipに変換
def convert_vmd_to_anim(vmd_path, output_path, fps=30, mesh_name="Body"):
    # VMDファイル読み込み
    vmd = read_vmd(vmd_path)

    # キーフレームをグループ化
    bone_curves = defaultdict(lambda: defaultdict(list))
    bone_slopes = defaultdict(lambda: defaultdict(list))
    morph_curves = defaultdict(list)
    max_frame = 0

    # ボーンキーフレーム処理（bone_mappingに存在するボーンのみ）
    for frame in vmd.boneframes:
        mmd_bone = frame.name
        if mmd_bone not in bone_mapping:
            continue  # bone_mappingにないボーンはスキップ
        unity_bone = bone_mapping[mmd_bone]
        time = frame.f / fps
        pos = frame.pos  # [x, y, z]
        rot = frame.rot  # [x, y, z] (degrees, Euler angles)

        # Position curves
        bone_curves[unity_bone]['position.x'].append((time, pos[0]))
        bone_curves[unity_bone]['position.y'].append((time, pos[1]))
        bone_curves[unity_bone]['position.z'].append((time, pos[2]))

        # Rotation curves (Euler angles, already in degrees)
        bone_curves[unity_bone]['rotation.x'].append((time, rot[0]))
        bone_curves[unity_bone]['rotation.y'].append((time, rot[1]))
        bone_curves[unity_bone]['rotation.z'].append((time, rot[2]))

        # 補間データの処理
        # 前のフレームとの差分を計算（最初のフレームはスキップ）
        prev_frame = next((f for f in vmd.boneframes if f.name == mmd_bone and f.f < frame.f), None)
        if prev_frame:
            delta_time = (frame.f - prev_frame.f) / fps
            delta_pos = [frame.pos[i] - prev_frame.pos[i] for i in range(3)]
            delta_rot = [frame.rot[i] - prev_frame.rot[i] for i in range(3)]
            # X, Y, Z, Rotationの補間
            in_slope_x, out_slope_x = get_slope_from_interpolation(frame.interp_x, delta_time, delta_pos[0])
            in_slope_y, out_slope_y = get_slope_from_interpolation(frame.interp_y, delta_time, delta_pos[1])
            in_slope_z, out_slope_z = get_slope_from_interpolation(frame.interp_z, delta_time, delta_pos[2])
            in_slope_r, out_slope_r = get_slope_from_interpolation(frame.interp_r, delta_time, delta_rot[0])  # 簡略化でrot[0]使用
            bone_slopes[unity_bone]['position.x'].append((in_slope_x, out_slope_x))
            bone_slopes[unity_bone]['position.y'].append((in_slope_y, out_slope_y))
            bone_slopes[unity_bone]['position.z'].append((in_slope_z, out_slope_z))
            bone_slopes[unity_bone]['rotation.x'].append((in_slope_r, out_slope_r))
            bone_slopes[unity_bone]['rotation.y'].append((in_slope_r, out_slope_r))
            bone_slopes[unity_bone]['rotation.z'].append((in_slope_r, out_slope_r))

        if frame.f > max_frame:
            max_frame = frame.f

    # モーフキーフレーム処理（VmdMorphFrameに基づく）
    for frame in vmd.morphframes:
        morph_name = frame.name
        time = frame.f / fps
        weight = frame.val * 100  # UnityのblendShapeは0-100スケール
        morph_curves[morph_name].append((time, weight))

        if frame.f > max_frame:
            max_frame = frame.f

    # .animファイル生成
    with open(output_path, 'w', encoding='utf-8') as f:
        f.write("%YAML 1.1\n")
        f.write("%TAG !u! tag:unity3d.com,2011:\n")
        f.write("--- !u!74 &7400000\n")
        f.write("AnimationClip:\n")
        f.write("  m_ObjectHideFlags: 0\n")
        f.write("  m_CorrespondingSourceObject: {fileID: 0}\n")
        f.write("  m_PrefabInstance: {fileID: 0}\n")
        f.write("  m_PrefabAsset: {fileID: 0}\n")
        f.write("  m_Name: MMDMotion\n")
        f.write("  serializedVersion: 7\n")
        f.write("  m_Legacy: 0\n")
        f.write("  m_Compressed: 0\n")
        f.write("  m_UseHighQualityCurve: 1\n")
        f.write("  m_RotationCurves: []\n")
        f.write("  m_CompressedRotationCurves: []\n")
        f.write("  m_EulerCurves: []\n")
        f.write("  m_PositionCurves: []\n")
        f.write("  m_ScaleCurves: []\n")
        f.write("  m_FloatCurves:\n")

        # ボーンのFloatCurves（bone_mappingに存在するボーンのみ）
        for bone, curves in bone_curves.items():
            path = f"Humanoid/{bone}"  # Humanoid階層のパス（モデルに合わせて調整）
            for prop in ['position.x', 'position.y', 'position.z', 'rotation.x', 'rotation.y', 'rotation.z']:
                if prop in curves:
                    attr = f"localPosition.{prop.split('.')[-1]}" if 'position' in prop else f"localEulerAnglesHint.{prop.split('.')[-1]}"
                    slopes = bone_slopes[bone][prop] if prop in bone_slopes[bone] else None
                    write_float_curve(f, path, attr, sorted(curves[prop]), slopes)

        # モーフのFloatCurves（VmdMorphFrameに基づく）
        for morph_name, keys in morph_curves.items():
            path = mesh_name  # SkinnedMeshRendererのパス
            prop = f"blendShape.{morph_name}"
            write_float_curve(f, path, prop, sorted(keys))  # モーフは線形補間（slopesなし）

        f.write("  m_PPtrCurves: []\n")
        f.write(f"  m_SampleRate: {fps}\n")
        f.write("  m_WrapMode: 0\n")
        f.write("  m_Bounds:\n")
        f.write("    m_Center: {x: 0, y: 0, z: 0}\n")
        f.write("    m_Extent: {x: 0, y: 0, z: 0}\n")
        f.write("  m_ClipBindingConstant:\n")
        f.write("    genericBindings: []\n")
        f.write("    pptrCurveMapping: []\n")
        f.write("  m_AnimationClipSettings:\n")
        f.write("    serializedVersion: 2\n")
        f.write("    m_AdditiveReferencePoseClip: {fileID: 0}\n")
        f.write("    m_AdditiveReferencePoseTime: 0\n")
        f.write("    m_StartTime: 0\n")
        f.write(f"    m_StopTime: {(max_frame / fps) if max_frame > 0 else 1}\n")
        f.write("    m_OrientationOffsetY: 0\n")
        f.write("    m_Level: 0\n")
        f.write("    m_CycleOffset: 0\n")
        f.write("    m_HasAdditiveReferencePose: 0\n")
        f.write("    m_LoopTime: false\n")
        f.write("    m_LoopBlend: false\n")
        f.write("    m_LoopBlendOrientation: false\n")
        f.write("    m_LoopBlendPositionY: false\n")
        f.write("    m_LoopBlendPositionXZ: false\n")
        f.write("    m_KeepOriginalOrientation: false\n")
        f.write("    m_KeepOriginalPositionY: true\n")
        f.write("    m_KeepOriginalPositionXZ: false\n")
        f.write("    m_HeightFromFeet: false\n")
        f.write("    m_Mirror: false\n")
        f.write("  m_EditorCurves: []\n")
        f.write("  m_EulerEditorCurves: []\n")
        f.write("  m_HasGenericRootTransform: 0\n")
        f.write("  m_HasMotionFloatCurves: 0\n")
        f.write("  m_Events: []\n")
# 使用例
if __name__ == "__main__":
    vmd_path = "./MmdMaterialConvert/bibbidibaFull_DanceMotion.vmd"  # VMDファイルのパス
    
    if  os.path.exists(vmd_path):
        output_path = "./MMDMotion.anim"  # 出力する.animファイルのパス
        convert_vmd_to_anim(vmd_path, output_path, fps=30, mesh_name="Body")
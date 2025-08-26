import sys
import io
import json
import os
from mmd_scripting.core.nuthouse01_pmx_parser import read_pmx

def main():
    if len(sys.argv) < 2:
        print("[]")
        return

    pmx_path = sys.argv[1]
    if not os.path.exists(pmx_path):
        print("[]")
        return

    # ログを一時的に無効化
    original_stdout = sys.stdout
    sys.stdout = io.StringIO()
    try:
        model = read_pmx(pmx_path)
    finally:
        sys.stdout = original_stdout

    if not model:
        print("[]")
        return

    materials_info = []
    for mat in model.materials:
        tex_name = os.path.basename(mat.tex_path) if getattr(mat, "tex_path", None) else ""
        materials_info.append({
            "name": mat.name_jp if hasattr(mat, "name_jp") else mat.name,
            "texture": tex_name
        })
    sys.stdout = io.TextIOWrapper(sys.stdout.buffer, encoding='utf-8')  # 追加
    # JSONだけを出力
    print(json.dumps(materials_info, ensure_ascii=False))

if __name__ == "__main__":
    main()

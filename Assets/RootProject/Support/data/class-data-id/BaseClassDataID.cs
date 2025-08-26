using System.IO;
    using System;

    namespace GameCore.Tables
    {
        public abstract class BaseClassDataID<T> where T : Enum
        {
            protected BaseClassDataID(BinaryReader reader)
            {
                // ここで共通の処理を追加可能（例：ヘッダチェックなど）
            }
        }
    }

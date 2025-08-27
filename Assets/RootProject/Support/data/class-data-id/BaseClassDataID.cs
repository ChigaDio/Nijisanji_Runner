using System.IO;
    using System;
    using System.Collections.Generic;

    namespace GameCore.Tables
    {
        public abstract class BaseClassDataID<T,E> where T : Enum where E : BaseClassDataRow
        {
            public static Dictionary<T,E> Table = new Dictionary<T,E>();
            protected BaseClassDataID(BinaryReader reader)
            {
                // ここで共通の処理を追加可能（例：ヘッダチェックなど）
            }
        }
    }

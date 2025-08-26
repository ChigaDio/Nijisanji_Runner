using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
namespace GameCore.Tables
{
    public class ConditionTypeTable : BaseClassDataID<ConditionTypeTableID, ConditionTypeRow>
    {
        public class ConditionTypeRow : BaseClassDataRow
        {
            private float adjustment;
            public float Adjustment { get => adjustment; }

            public override void Read(BinaryReader reader)
            {
                adjustment = reader.ReadSingle();
            }
        }

        public ConditionTypeTable(BinaryReader reader) : base(reader)
        {
            int rowCount = reader.ReadInt32();
            int colCount = reader.ReadInt32();
            var colNames = new string[colCount];
            var colTypes = new string[colCount];
            for(int i=0; i<colCount; i++) {
                int len = reader.ReadInt32();
                colNames[i] = System.Text.Encoding.UTF8.GetString(reader.ReadBytes(len));
                len = reader.ReadInt32();
                colTypes[i] = System.Text.Encoding.UTF8.GetString(reader.ReadBytes(len));
            }
            for(int r=0; r<rowCount; r++) {
                var enumVal = (ConditionTypeTableID)Enum.ToObject(typeof(ConditionTypeTableID), reader.ReadInt32());
                var row = new ConditionTypeRow();
                row.Read(reader);
                Table[enumVal] = row;
            }
        }
    }
}

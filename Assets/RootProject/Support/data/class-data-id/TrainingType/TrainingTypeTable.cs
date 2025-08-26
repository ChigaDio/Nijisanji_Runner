using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
namespace GameCore.Tables
{
    public class TrainingTypeTable : BaseClassDataID<TrainingTypeTableID, TrainingTypeRow>
    {
        public class TrainingTypeRow : BaseClassDataRow
        {
            private float adjustment;
            public float Adjustment { get => adjustment; }

            public override void Read(BinaryReader reader)
            {
                adjustment = reader.ReadSingle();
            }
        }

        public TrainingTypeTable(BinaryReader reader) : base(reader)
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
                var enumVal = (TrainingTypeTableID)Enum.ToObject(typeof(TrainingTypeTableID), reader.ReadInt32());
                var row = new TrainingTypeRow();
                row.Read(reader);
                Table[enumVal] = row;
            }
        }
    }
}

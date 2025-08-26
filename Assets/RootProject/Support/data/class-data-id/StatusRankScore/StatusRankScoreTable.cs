using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
namespace GameCore.Tables
{
    public class StatusRankScoreTable : BaseClassDataID<StatusRankScoreTableID, StatusRankScoreRow>
    {
        public class StatusRankScoreRow : BaseClassDataRow
        {
            private int performanceScore;
            public int Performancescore { get => performanceScore; }
            private float rating;
            public float Rating { get => rating; }

            public override void Read(BinaryReader reader)
            {
                performanceScore = reader.ReadInt32();
                rating = reader.ReadSingle();
            }
        }

        public StatusRankScoreTable(BinaryReader reader) : base(reader)
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
                var enumVal = (StatusRankScoreTableID)Enum.ToObject(typeof(StatusRankScoreTableID), reader.ReadInt32());
                var row = new StatusRankScoreRow();
                row.Read(reader);
                Table[enumVal] = row;
            }
        }
    }
}

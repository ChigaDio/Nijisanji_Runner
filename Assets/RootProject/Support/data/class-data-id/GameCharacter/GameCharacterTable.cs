using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
namespace GameCore.Tables
{
    public class GameCharacterTable : BaseClassDataID<GameCharacterTableID>
    {
        public static Dictionary<GameCharacterTableID, GameCharacterRow> Table = new Dictionary<GameCharacterTableID, GameCharacterRow>();

        public class GameCharacterRow : BaseClassDataRow
        {
            private float baseSpeed;
            public float Basespeed { get => baseSpeed; }
            private float baseStamina;
            public float Basestamina { get => baseStamina; }
            private float basePower;
            public float Basepower { get => basePower; }
            private float baseGuts;
            public float Baseguts { get => baseGuts; }
            private float baseWisdom;
            public float Basewisdom { get => baseWisdom; }
            private string prefabName;
            public string Prefabname { get => prefabName; }
            private float growthSpeed;
            public float Growthspeed { get => growthSpeed; }
            private float growthStamina;
            public float Growthstamina { get => growthStamina; }
            private float growthPower;
            public float Growthpower { get => growthPower; }
            private float growthGuts;
            public float Growthguts { get => growthGuts; }
            private float growthWisdom;
            public float Growthwisdom { get => growthWisdom; }

            public override void Read(BinaryReader reader)
            {
                baseSpeed = reader.ReadSingle();
                baseStamina = reader.ReadSingle();
                basePower = reader.ReadSingle();
                baseGuts = reader.ReadSingle();
                baseWisdom = reader.ReadSingle();
                int len5 = reader.ReadInt32();
                prefabName = System.Text.Encoding.UTF8.GetString(reader.ReadBytes(len5));
                growthSpeed = reader.ReadSingle();
                growthStamina = reader.ReadSingle();
                growthPower = reader.ReadSingle();
                growthGuts = reader.ReadSingle();
                growthWisdom = reader.ReadSingle();
            }
        }

        public GameCharacterTable(BinaryReader reader) : base(reader)
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
                var enumVal = (GameCharacterTableID)Enum.ToObject(typeof(GameCharacterTableID), reader.ReadInt32());
                var row = new GameCharacterRow();
                row.Read(reader);
                Table[enumVal] = row;
            }
        }
    }
}

using System;
using UnityEngine;
using GameCore.Tables;
using GameCore.Tables.ID;

namespace GameCore.Tables
{
    public static class ConditionTypeIDExtensions
    {
        public static ConditionTypeRow GetRow(this ConditionTypeTableID id)
        {
            if (ConditionTypeTable.Table.TryGetValue(id, out var row))
            {
                return row;
            }
            else
            {
                return null; // または throw new KeyNotFoundException()
            }
        }
    }
}

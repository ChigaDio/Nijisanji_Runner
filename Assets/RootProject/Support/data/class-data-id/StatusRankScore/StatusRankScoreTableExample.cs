using System;
using UnityEngine;
using GameCore.Tables;
using GameCore.Tables.ID;

namespace GameCore.Tables
{
    public static class StatusRankScoreIDExtensions
    {
        public static StatusRankScoreRow GetRow(this StatusRankScoreTableID id)
        {
            if (StatusRankScoreTable.Table.TryGetValue(id, out var row))
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

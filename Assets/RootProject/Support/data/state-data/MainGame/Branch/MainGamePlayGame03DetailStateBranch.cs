using System;
using UnityEngine;
using GameCore.States.ID;
using GameCore.States.Managers;

namespace GameCore.States.Branch
{
    public class MainGamePlayGame03DetailStateBranch : BaseMainGamePlayGame03DetailStateBranch
    {
        public override bool MainGamePlayGame_to_ExitGame04(MainGameStateManagerData manager_data, MainGamePlayGameState state)
        {
            return false;
        }

        public override bool MainGamePlayGame_to_TitleGame02(MainGameStateManagerData manager_data, MainGamePlayGameState state)
        {
            return false;
        }

    }
}

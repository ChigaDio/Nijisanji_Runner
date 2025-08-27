using System;
using UnityEngine;
using GameCore.States.ID;
using GameCore.States.Managers;

namespace GameCore.States.Branch
{
    public class MainGameTitleGame02DetailStateBranch : BaseMainGameTitleGame02DetailStateBranch
    {
        public override bool MainGameTitleGame_to_PlayGame03(MainGameStateManagerData manager_data, MainGameTitleGameState state)
        {
            return false;
        }

        public override bool MainGameTitleGame_to_ExitGame04(MainGameStateManagerData manager_data, MainGameTitleGameState state)
        {
            return false;
        }

    }
}

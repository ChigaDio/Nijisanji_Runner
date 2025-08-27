using System;
using UnityEngine;
using GameCore.States.ID;
using GameCore.States.Managers;

namespace GameCore.States.Branch
{
    public abstract class BaseMainGameTitleGame02DetailStateBranch : BaseMainGameTitleGameDetailStateBranch
    {
        public override MainGameStateID ConditionsBranch(MainGameStateManagerData manager_data, MainGameTitleGameState state)
        {
            if (MainGameTitleGame_to_PlayGame03(manager_data, state))
                return MainGameStateID.PlayGame03;
            if (MainGameTitleGame_to_ExitGame04(manager_data, state))
                return MainGameStateID.ExitGame04;
            return MainGameStateID.None;
        }

        public override abstract bool MainGameTitleGame_to_PlayGame03(MainGameStateManagerData manager_data, MainGameTitleGameState state);
        public override abstract bool MainGameTitleGame_to_ExitGame04(MainGameStateManagerData manager_data, MainGameTitleGameState state);
    }
}

using System;
using UnityEngine;
using GameCore.States.ID;
using GameCore.States.Managers;

namespace GameCore.States.Branch
{
    public abstract class BaseMainGamePlayGame03DetailStateBranch : BaseMainGamePlayGameDetailStateBranch
    {
        public override MainGameStateID ConditionsBranch(MainGameStateManagerData manager_data, MainGamePlayGameState state)
        {
            if (MainGamePlayGame_to_ExitGame04(manager_data, state))
                return MainGameStateID.ExitGame04;
            if (MainGamePlayGame_to_TitleGame02(manager_data, state))
                return MainGameStateID.TitleGame02;
            return MainGameStateID.None;
        }

        public override abstract bool MainGamePlayGame_to_ExitGame04(MainGameStateManagerData manager_data, MainGamePlayGameState state);
        public override abstract bool MainGamePlayGame_to_TitleGame02(MainGameStateManagerData manager_data, MainGamePlayGameState state);
    }
}

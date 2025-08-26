using System.Collections.Generic;
using UnityEngine;

namespace GameCore.States.Branch
{
    public abstract class BaseMainGamePlayGame03DetailStateBranch : BaseMainGamePlayGameDetailStateBranch
    {
        public override GameCore.States.ID.MainGameStateID ConditionsBranch(GameCore.States.Managers.MainGameStateManagerData manager_data, GameCore.States.MainGamePlayGameState state)
        {
            if (MainGamePlayGame_to_ExitGame04(manager_data, state))
                return GameCore.States.ID.MainGameStateID.ExitGame04;
            if (MainGamePlayGame_to_TitleGame02(manager_data, state))
                return GameCore.States.ID.MainGameStateID.TitleGame02;
            return GameCore.States.ID.MainGameStateID.None;
        }

        public abstract bool MainGamePlayGame_to_ExitGame04(GameCore.States.Managers.MainGameStateManagerData manager_data, GameCore.States.MainGamePlayGameState state);
        public abstract bool MainGamePlayGame_to_TitleGame02(GameCore.States.Managers.MainGameStateManagerData manager_data, GameCore.States.MainGamePlayGameState state);
    }
}

using System.Collections.Generic;
using UnityEngine;

namespace GameCore.States.Branch
{
    public abstract class BaseMainGameTitleGame02DetailStateBranch : BaseMainGameTitleGameDetailStateBranch
    {
        public override GameCore.States.ID.MainGameStateID ConditionsBranch(GameCore.States.Managers.MainGameStateManagerData manager_data, GameCore.States.MainGameTitleGameState state)
        {
            if (MainGameTitleGame_to_PlayGame03(manager_data, state))
                return GameCore.States.ID.MainGameStateID.PlayGame03;
            if (MainGameTitleGame_to_ExitGame04(manager_data, state))
                return GameCore.States.ID.MainGameStateID.ExitGame04;
            return GameCore.States.ID.MainGameStateID.None;
        }

        public abstract bool MainGameTitleGame_to_PlayGame03(GameCore.States.Managers.MainGameStateManagerData manager_data, GameCore.States.MainGameTitleGameState state);
        public abstract bool MainGameTitleGame_to_ExitGame04(GameCore.States.Managers.MainGameStateManagerData manager_data, GameCore.States.MainGameTitleGameState state);
    }
}

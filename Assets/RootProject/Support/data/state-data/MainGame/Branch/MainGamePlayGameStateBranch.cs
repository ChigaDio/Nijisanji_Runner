using System.Collections.Generic;
using UnityEngine;

namespace GameCore.States.Branch
{
    public class MainGamePlayGameStateBranch : GameCore.States.BaseMainGameStateBranch<MainGamePlayGameState>
    {
        public override GameCore.States.ID.MainGameStateID ConditionsBranch(GameCore.States.Managers.MainGameStateManagerData manager_data, MainGamePlayGameState state)
        {
            var id = manager_data.GetNowID();
            var branch = Factory(id);
            return branch != null ? branch.ConditionsBranch(manager_data, state) : GameCore.States.ID.MainGameStateID.None;
        }

        public BaseMainGameDetailStateBranch Factory(GameCore.States.ID.MainGameStateID id)
        {
            switch (id)
            {
                case GameCore.States.ID.MainGameStateID.PlayGame03:
                    return new MainGamePlayGame03DetailStateBranch();
                default:
                    return null;
            }
        }
    }
}

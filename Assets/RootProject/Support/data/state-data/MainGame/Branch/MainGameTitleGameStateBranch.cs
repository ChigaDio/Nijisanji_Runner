using System.Collections.Generic;
using UnityEngine;

namespace GameCore.States.Branch
{
    public class MainGameTitleGameStateBranch : GameCore.States.BaseMainGameStateBranch<MainGameTitleGameState>
    {
        public override GameCore.States.ID.MainGameStateID ConditionsBranch(GameCore.States.Managers.MainGameStateManagerData manager_data, MainGameTitleGameState state)
        {
            var id = manager_data.GetNowID();
            var branch = Factory(id);
            return branch != null ? branch.ConditionsBranch(manager_data, state) : GameCore.States.ID.MainGameStateID.None;
        }

        public BaseMainGameDetailStateBranch Factory(GameCore.States.ID.MainGameStateID id)
        {
            switch (id)
            {
                case GameCore.States.ID.MainGameStateID.TitleGame02:
                    return new MainGameTitleGame02DetailStateBranch();
                default:
                    return null;
            }
        }
    }
}

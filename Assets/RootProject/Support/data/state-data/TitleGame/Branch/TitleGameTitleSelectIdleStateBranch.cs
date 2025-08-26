using System.Collections.Generic;
using UnityEngine;

namespace GameCore.States.Branch
{
    public class TitleGameTitleSelectIdleStateBranch : GameCore.States.BaseTitleGameStateBranch<TitleGameTitleSelectIdleState>
    {
        public override GameCore.States.ID.TitleGameStateID ConditionsBranch(GameCore.States.Managers.TitleGameStateManagerData manager_data, TitleGameTitleSelectIdleState state)
        {
            var id = manager_data.GetNowID();
            var branch = Factory(id);
            return branch != null ? branch.ConditionsBranch(manager_data, state) : GameCore.States.ID.TitleGameStateID.None;
        }

        public BaseTitleGameDetailStateBranch Factory(GameCore.States.ID.TitleGameStateID id)
        {
            switch (id)
            {
                case GameCore.States.ID.TitleGameStateID.TitleSelectIdle05:
                    return new TitleGameTitleSelectIdle05DetailStateBranch();
                default:
                    return null;
            }
        }
    }
}

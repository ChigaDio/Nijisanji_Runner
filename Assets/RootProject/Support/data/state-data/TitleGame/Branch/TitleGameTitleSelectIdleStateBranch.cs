using System;
using UnityEngine;
using GameCore.States.ID;
using GameCore.States.Managers;

namespace GameCore.States.Branch
{
    public class TitleGameTitleSelectIdleStateBranch : BaseTitleGameStateBranch<TitleGameTitleSelectIdleState, BaseTitleGameTitleSelectIdleDetailStateBranch>
    {
        public override TitleGameStateID ConditionsBranch(TitleGameStateManagerData manager_data, TitleGameTitleSelectIdleState state)
        {
            var id = manager_data.GetNowStateID();
            var branch = Factory(id);
            return branch != null ? branch.ConditionsBranch(manager_data, state) : TitleGameStateID.None;
        }

        public override BaseTitleGameTitleSelectIdleDetailStateBranch Factory(TitleGameStateID id)
        {
            switch (id)
            {
                case TitleGameStateID.TitleSelectIdle05:
                    return new TitleGameTitleSelectIdle05DetailStateBranch();
                default:
                    return null;
            }
        }
    }
}

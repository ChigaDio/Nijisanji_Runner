using System;
using UnityEngine;
using GameCore.States.ID;
using GameCore.States.Managers;

namespace GameCore.States.Branch
{
    public class MainGameTitleGameStateBranch : BaseMainGameStateBranch<MainGameTitleGameState, BaseMainGameTitleGameDetailStateBranch>
    {
        public override MainGameStateID ConditionsBranch(MainGameStateManagerData manager_data, MainGameTitleGameState state)
        {
            var id = manager_data.GetNowStateID();
            var branch = Factory(id);
            return branch != null ? branch.ConditionsBranch(manager_data, state) : MainGameStateID.None;
        }

        public override BaseMainGameTitleGameDetailStateBranch Factory(MainGameStateID id)
        {
            switch (id)
            {
                case MainGameStateID.TitleGame02:
                    return new MainGameTitleGame02DetailStateBranch();
                default:
                    return null;
            }
        }
    }
}

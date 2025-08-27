using System;
using UnityEngine;
using GameCore.States.ID;
using GameCore.States.Managers;

namespace GameCore.States.Branch
{
    public class MainGamePlayGameStateBranch : BaseMainGameStateBranch<MainGamePlayGameState, BaseMainGamePlayGameDetailStateBranch>
    {
        public override MainGameStateID ConditionsBranch(MainGameStateManagerData manager_data, MainGamePlayGameState state)
        {
            var id = manager_data.GetNowStateID();
            var branch = Factory(id);
            return branch != null ? branch.ConditionsBranch(manager_data, state) : MainGameStateID.None;
        }

        public override BaseMainGamePlayGameDetailStateBranch Factory(MainGameStateID id)
        {
            switch (id)
            {
                case MainGameStateID.PlayGame03:
                    return new MainGamePlayGame03DetailStateBranch();
                default:
                    return null;
            }
        }
    }
}

using UnityEngine;
using GameCore.States.Branch;

namespace GameCore.States
{
    public abstract class BaseMainGameTitleGameState : GameCore.States.BaseMainGameState
    {
        public override GameCore.States.ID.MainGameStateID BranchNextState(GameCore.States.Managers.MainGameStateManagerData state_manager_data)
        {
            var branch = new MainGameTitleGameStateBranch();
            var next_id = branch.ConditionsBranch(state_manager_data, this);
            return next_id;
        }
    }
}

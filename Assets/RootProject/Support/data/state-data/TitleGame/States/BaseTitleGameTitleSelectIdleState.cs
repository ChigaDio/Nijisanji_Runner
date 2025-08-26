using UnityEngine;
using GameCore.States.Branch;

namespace GameCore.States
{
    public abstract class BaseTitleGameTitleSelectIdleState : GameCore.States.BaseTitleGameState
    {
        public override GameCore.States.ID.TitleGameStateID BranchNextState(GameCore.States.Managers.TitleGameStateManagerData state_manager_data)
        {
            var branch = new TitleGameTitleSelectIdleStateBranch();
            var next_id = branch.ConditionsBranch(state_manager_data, this);
            return next_id;
        }
    }
}

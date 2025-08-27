using GameCore.States.Branch;
using UnityEngine;

namespace GameCore.States
{
    public class TitleGameTitleSelectIdleState : BaseTitleGameTitleSelectIdleState
    {
        public override void Enter(GameCore.States.Managers.TitleGameStateManagerData state_manager_data) { }
        public override void Update(GameCore.States.Managers.TitleGameStateManagerData state_manager_data) { }
        public override void Exit(GameCore.States.Managers.TitleGameStateManagerData state_manager_data) { }
        public override GameCore.States.ID.TitleGameStateID BranchNextState(GameCore.States.Managers.TitleGameStateManagerData state_manager_data)
        {
            var branch = new TitleGameTitleSelectIdleStateBranch();
            var next_id = branch.ConditionsBranch(state_manager_data, this);
            return next_id;
        }
    }
}

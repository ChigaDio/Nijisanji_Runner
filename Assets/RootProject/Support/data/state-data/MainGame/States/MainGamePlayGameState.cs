using GameCore.States.Branch;
using UnityEngine;

namespace GameCore.States
{
    public class MainGamePlayGameState : BaseMainGamePlayGameState
    {
        public override void Enter(GameCore.States.Managers.MainGameStateManagerData state_manager_data) { }
        public override void Update(GameCore.States.Managers.MainGameStateManagerData state_manager_data) { }
        public override void Exit(GameCore.States.Managers.MainGameStateManagerData state_manager_data) { }
        public override GameCore.States.ID.MainGameStateID BranchNextState(GameCore.States.Managers.MainGameStateManagerData state_manager_data)
        {
            var branch = new MainGamePlayGameStateBranch();
            var next_id = branch.ConditionsBranch(state_manager_data, this);
            return next_id;
        }
    }
}

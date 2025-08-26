using System.Collections.Generic;
using UnityEngine;

namespace GameCore.States.Branch
{
    public abstract class BaseMainGameDetailStateBranch<F> : BaseDetailStateBranch<GameCore.States.ID.MainGameStateID, GameCore.States.Managers.MainGameStateManagerData, F> where F : GameCore.States.BaseMainGameState
    {
        public abstract GameCore.States.ID.MainGameStateID ConditionsBranch(GameCore.States.Managers.MainGameStateManagerData manager_data, F state);
    }
}

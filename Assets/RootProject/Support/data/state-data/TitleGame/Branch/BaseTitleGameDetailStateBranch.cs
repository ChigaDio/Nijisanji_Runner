using System.Collections.Generic;
using UnityEngine;

namespace GameCore.States.Branch
{
    public abstract class BaseTitleGameDetailStateBranch<F> : BaseDetailStateBranch<GameCore.States.ID.TitleGameStateID, GameCore.States.Managers.TitleGameStateManagerData, F> where F : GameCore.States.BaseTitleGameState
    {
        public abstract GameCore.States.ID.TitleGameStateID ConditionsBranch(GameCore.States.Managers.TitleGameStateManagerData manager_data, F state);
    }
}

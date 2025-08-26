using System.Collections.Generic;
using UnityEngine;

namespace GameCore.States.Branch
{
    public abstract class BaseMainGameTitleGameDetailStateBranch : BaseMainGameDetailStateBranch<MainGameTitleGameState>
    {
        public abstract GameCore.States.ID.MainGameStateID ConditionsBranch(GameCore.States.Managers.MainGameStateManagerData manager_data, GameCore.States.MainGameTitleGameState state);
    }
}

using System.Collections.Generic;
using UnityEngine;

namespace GameCore.States.Branch
{
    public abstract class BaseMainGamePlayGameDetailStateBranch : BaseMainGameDetailStateBranch<MainGamePlayGameState>
    {
        public abstract GameCore.States.ID.MainGameStateID ConditionsBranch(GameCore.States.Managers.MainGameStateManagerData manager_data, GameCore.States.MainGamePlayGameState state);
    }
}

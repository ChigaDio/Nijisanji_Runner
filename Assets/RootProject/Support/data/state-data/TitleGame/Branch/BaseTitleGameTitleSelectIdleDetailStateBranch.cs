using System.Collections.Generic;
using UnityEngine;

namespace GameCore.States.Branch
{
    public abstract class BaseTitleGameTitleSelectIdleDetailStateBranch : BaseTitleGameDetailStateBranch<TitleGameTitleSelectIdleState>
    {
        public abstract GameCore.States.ID.TitleGameStateID ConditionsBranch(GameCore.States.Managers.TitleGameStateManagerData manager_data, GameCore.States.TitleGameTitleSelectIdleState state);
    }
}

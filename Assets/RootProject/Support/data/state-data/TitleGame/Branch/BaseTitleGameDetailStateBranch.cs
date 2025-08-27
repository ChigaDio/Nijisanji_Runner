using System;
using UnityEngine;
using GameCore.States.ID;
using GameCore.States.Managers;

namespace GameCore.States.Branch
{
    public abstract class BaseTitleGameDetailStateBranch<TState> : BaseDetailStateBranch<TitleGameStateID, TitleGameStateManagerData, TState>
        where TState : GameCore.States.BaseTitleGameState
    {
        public override abstract TitleGameStateID ConditionsBranch(TitleGameStateManagerData manager_data, TState state);
    }
}

using System;
using UnityEngine;
using GameCore.States.Managers;

using GameCore.States.ID;

namespace GameCore.States.Branch
{
    public abstract class BaseTitleGameStateBranch<TState, TDetailState> : BaseStateBranch<TitleGameStateID, TitleGameStateManagerData, TState, TDetailState>
        where TState : GameCore.States.BaseTitleGameState
        where TDetailState : BaseTitleGameDetailStateBranch<TState>
    {
        public override abstract TitleGameStateID ConditionsBranch(TitleGameStateManagerData manager_data, TState state);
        public override abstract TDetailState Factory(TitleGameStateID id);
    }
}

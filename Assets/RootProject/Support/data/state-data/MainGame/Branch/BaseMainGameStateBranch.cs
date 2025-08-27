using System;
using UnityEngine;
using GameCore.States.Managers;

using GameCore.States.ID;


namespace GameCore.States.Branch
{
    public abstract class BaseMainGameStateBranch<TState, TDetailState> : BaseStateBranch<MainGameStateID, MainGameStateManagerData, TState, TDetailState>
        where TState : GameCore.States.BaseMainGameState
        where TDetailState : BaseMainGameDetailStateBranch<TState>
    {
        public override abstract MainGameStateID ConditionsBranch(MainGameStateManagerData manager_data, TState state);
        public override abstract TDetailState Factory(MainGameStateID id);
    }
}

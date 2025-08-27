using System;
using UnityEngine;
using GameCore.States.ID;
using GameCore.States.Managers;

namespace GameCore.States.Branch
{
    public abstract class BaseMainGameDetailStateBranch<TState> : BaseDetailStateBranch<MainGameStateID, MainGameStateManagerData, TState>
        where TState : GameCore.States.BaseMainGameState
    {
        public override abstract MainGameStateID ConditionsBranch(MainGameStateManagerData manager_data, TState state);
    }
}

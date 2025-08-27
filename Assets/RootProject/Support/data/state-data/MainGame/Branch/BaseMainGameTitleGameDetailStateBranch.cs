using System;
using UnityEngine;
using GameCore.States.ID;
using GameCore.States.Managers;

namespace GameCore.States.Branch
{
    public abstract class BaseMainGameTitleGameDetailStateBranch : BaseMainGameDetailStateBranch<MainGameTitleGameState>
    {
        public override abstract MainGameStateID ConditionsBranch(MainGameStateManagerData manager_data, MainGameTitleGameState state);
        public abstract bool MainGameTitleGame_to_PlayGame03(MainGameStateManagerData manager_data, MainGameTitleGameState state);
        public abstract bool MainGameTitleGame_to_ExitGame04(MainGameStateManagerData manager_data, MainGameTitleGameState state);
    }
}

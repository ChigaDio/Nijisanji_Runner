using System;
using UnityEngine;
using GameCore.States.ID;
using GameCore.States.Managers;

namespace GameCore.States.Branch
{
    public abstract class BaseMainGamePlayGameDetailStateBranch : BaseMainGameDetailStateBranch<MainGamePlayGameState>
    {
        public override abstract MainGameStateID ConditionsBranch(MainGameStateManagerData manager_data, MainGamePlayGameState state);
        public abstract bool MainGamePlayGame_to_ExitGame04(MainGameStateManagerData manager_data, MainGamePlayGameState state);
        public abstract bool MainGamePlayGame_to_TitleGame02(MainGameStateManagerData manager_data, MainGamePlayGameState state);
    }
}

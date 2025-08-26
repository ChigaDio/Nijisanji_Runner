using System.Collections.Generic;
using UnityEngine;

namespace GameCore.States.Branch
{
    public class MainGamePlayGame03DetailStateBranch : BaseMainGamePlayGame03DetailStateBranch
    {
        public override bool MainGamePlayGame_to_ExitGame04(GameCore.States.Managers.MainGameStateManagerData manager_data, MainGamePlayGameState state)
        {
            return false;
        }

        public override bool MainGamePlayGame_to_TitleGame02(GameCore.States.Managers.MainGameStateManagerData manager_data, MainGamePlayGameState state)
        {
            return false;
        }

    }
}

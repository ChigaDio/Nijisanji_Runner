using System.Collections.Generic;
using UnityEngine;

namespace GameCore.States.Branch
{
    public class MainGameTitleGame02DetailStateBranch : BaseMainGameTitleGame02DetailStateBranch
    {
        public override bool MainGameTitleGame_to_PlayGame03(GameCore.States.Managers.MainGameStateManagerData manager_data, MainGameTitleGameState state)
        {
            return false;
        }

        public override bool MainGameTitleGame_to_ExitGame04(GameCore.States.Managers.MainGameStateManagerData manager_data, MainGameTitleGameState state)
        {
            return false;
        }

    }
}

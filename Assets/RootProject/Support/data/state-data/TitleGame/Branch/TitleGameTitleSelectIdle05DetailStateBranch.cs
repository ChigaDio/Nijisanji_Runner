using System.Collections.Generic;
using UnityEngine;

namespace GameCore.States.Branch
{
    public class TitleGameTitleSelectIdle05DetailStateBranch : BaseTitleGameTitleSelectIdle05DetailStateBranch
    {
        public override bool TitleGameTitleSelectIdle_to_InitGame06(GameCore.States.Managers.TitleGameStateManagerData manager_data, TitleGameTitleSelectIdleState state)
        {
            return false;
        }

        public override bool TitleGameTitleSelectIdle_to_LoadGame07(GameCore.States.Managers.TitleGameStateManagerData manager_data, TitleGameTitleSelectIdleState state)
        {
            return false;
        }

        public override bool TitleGameTitleSelectIdle_to_BackTitleStartIdle08(GameCore.States.Managers.TitleGameStateManagerData manager_data, TitleGameTitleSelectIdleState state)
        {
            return false;
        }

        public override bool TitleGameTitleSelectIdle_to_ExitGame09(GameCore.States.Managers.TitleGameStateManagerData manager_data, TitleGameTitleSelectIdleState state)
        {
            return false;
        }

        public override bool TitleGameTitleSelectIdle_to_OptionGame10(GameCore.States.Managers.TitleGameStateManagerData manager_data, TitleGameTitleSelectIdleState state)
        {
            return false;
        }

        public override bool TitleGameTitleSelectIdle_to_LicenseShow11(GameCore.States.Managers.TitleGameStateManagerData manager_data, TitleGameTitleSelectIdleState state)
        {
            return false;
        }

    }
}

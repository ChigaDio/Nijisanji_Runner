using System.Collections.Generic;
using UnityEngine;

namespace GameCore.States.Branch
{
    public abstract class BaseTitleGameTitleSelectIdle05DetailStateBranch : BaseTitleGameTitleSelectIdleDetailStateBranch
    {
        public override GameCore.States.ID.TitleGameStateID ConditionsBranch(GameCore.States.Managers.TitleGameStateManagerData manager_data, GameCore.States.TitleGameTitleSelectIdleState state)
        {
            if (TitleGameTitleSelectIdle_to_InitGame06(manager_data, state))
                return GameCore.States.ID.TitleGameStateID.InitGame06;
            if (TitleGameTitleSelectIdle_to_LoadGame07(manager_data, state))
                return GameCore.States.ID.TitleGameStateID.LoadGame07;
            if (TitleGameTitleSelectIdle_to_BackTitleStartIdle08(manager_data, state))
                return GameCore.States.ID.TitleGameStateID.BackTitleStartIdle08;
            if (TitleGameTitleSelectIdle_to_ExitGame09(manager_data, state))
                return GameCore.States.ID.TitleGameStateID.ExitGame09;
            if (TitleGameTitleSelectIdle_to_OptionGame10(manager_data, state))
                return GameCore.States.ID.TitleGameStateID.OptionGame10;
            if (TitleGameTitleSelectIdle_to_LicenseShow11(manager_data, state))
                return GameCore.States.ID.TitleGameStateID.LicenseShow11;
            return GameCore.States.ID.TitleGameStateID.None;
        }

        public abstract bool TitleGameTitleSelectIdle_to_InitGame06(GameCore.States.Managers.TitleGameStateManagerData manager_data, GameCore.States.TitleGameTitleSelectIdleState state);
        public abstract bool TitleGameTitleSelectIdle_to_LoadGame07(GameCore.States.Managers.TitleGameStateManagerData manager_data, GameCore.States.TitleGameTitleSelectIdleState state);
        public abstract bool TitleGameTitleSelectIdle_to_BackTitleStartIdle08(GameCore.States.Managers.TitleGameStateManagerData manager_data, GameCore.States.TitleGameTitleSelectIdleState state);
        public abstract bool TitleGameTitleSelectIdle_to_ExitGame09(GameCore.States.Managers.TitleGameStateManagerData manager_data, GameCore.States.TitleGameTitleSelectIdleState state);
        public abstract bool TitleGameTitleSelectIdle_to_OptionGame10(GameCore.States.Managers.TitleGameStateManagerData manager_data, GameCore.States.TitleGameTitleSelectIdleState state);
        public abstract bool TitleGameTitleSelectIdle_to_LicenseShow11(GameCore.States.Managers.TitleGameStateManagerData manager_data, GameCore.States.TitleGameTitleSelectIdleState state);
    }
}

using System;
using UnityEngine;
using GameCore.States.ID;
using GameCore.States.Managers;

namespace GameCore.States.Branch
{
    public abstract class BaseTitleGameTitleSelectIdle05DetailStateBranch : BaseTitleGameTitleSelectIdleDetailStateBranch
    {
        public override TitleGameStateID ConditionsBranch(TitleGameStateManagerData manager_data, TitleGameTitleSelectIdleState state)
        {
            if (TitleGameTitleSelectIdle_to_InitGame06(manager_data, state))
                return TitleGameStateID.InitGame06;
            if (TitleGameTitleSelectIdle_to_LoadGame07(manager_data, state))
                return TitleGameStateID.LoadGame07;
            if (TitleGameTitleSelectIdle_to_BackTitleStartIdle08(manager_data, state))
                return TitleGameStateID.BackTitleStartIdle08;
            if (TitleGameTitleSelectIdle_to_ExitGame09(manager_data, state))
                return TitleGameStateID.ExitGame09;
            if (TitleGameTitleSelectIdle_to_OptionGame10(manager_data, state))
                return TitleGameStateID.OptionGame10;
            if (TitleGameTitleSelectIdle_to_LicenseShow11(manager_data, state))
                return TitleGameStateID.LicenseShow11;
            return TitleGameStateID.None;
        }

        public override abstract bool TitleGameTitleSelectIdle_to_InitGame06(TitleGameStateManagerData manager_data, TitleGameTitleSelectIdleState state);
        public override abstract bool TitleGameTitleSelectIdle_to_LoadGame07(TitleGameStateManagerData manager_data, TitleGameTitleSelectIdleState state);
        public override abstract bool TitleGameTitleSelectIdle_to_BackTitleStartIdle08(TitleGameStateManagerData manager_data, TitleGameTitleSelectIdleState state);
        public override abstract bool TitleGameTitleSelectIdle_to_ExitGame09(TitleGameStateManagerData manager_data, TitleGameTitleSelectIdleState state);
        public override abstract bool TitleGameTitleSelectIdle_to_OptionGame10(TitleGameStateManagerData manager_data, TitleGameTitleSelectIdleState state);
        public override abstract bool TitleGameTitleSelectIdle_to_LicenseShow11(TitleGameStateManagerData manager_data, TitleGameTitleSelectIdleState state);
    }
}

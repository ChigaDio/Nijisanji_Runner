using System;
using UnityEngine;
using GameCore.States.ID;
using GameCore.States.Managers;

namespace GameCore.States.Branch
{
    public abstract class BaseTitleGameTitleSelectIdleDetailStateBranch : BaseTitleGameDetailStateBranch<TitleGameTitleSelectIdleState>
    {
        public override abstract TitleGameStateID ConditionsBranch(TitleGameStateManagerData manager_data, TitleGameTitleSelectIdleState state);
        public abstract bool TitleGameTitleSelectIdle_to_InitGame06(TitleGameStateManagerData manager_data, TitleGameTitleSelectIdleState state);
        public abstract bool TitleGameTitleSelectIdle_to_LoadGame07(TitleGameStateManagerData manager_data, TitleGameTitleSelectIdleState state);
        public abstract bool TitleGameTitleSelectIdle_to_BackTitleStartIdle08(TitleGameStateManagerData manager_data, TitleGameTitleSelectIdleState state);
        public abstract bool TitleGameTitleSelectIdle_to_ExitGame09(TitleGameStateManagerData manager_data, TitleGameTitleSelectIdleState state);
        public abstract bool TitleGameTitleSelectIdle_to_OptionGame10(TitleGameStateManagerData manager_data, TitleGameTitleSelectIdleState state);
        public abstract bool TitleGameTitleSelectIdle_to_LicenseShow11(TitleGameStateManagerData manager_data, TitleGameTitleSelectIdleState state);
    }
}

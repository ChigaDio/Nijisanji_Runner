using System;
using UnityEngine;
using GameCore.States.ID;
using GameCore.States.Managers;
using GameCore.States;

namespace GameCore.States.Control
{
    public abstract class BaseTitleGameStateControl
        : BaseStateControl<TitleGameStateID, TitleGameStateManagerData, BaseTitleGameState>
    {
        protected override TitleGameStateID GetInitStartID()
        {
            return TitleGameStateID.BeginTitle01;
        }

        public override void BranchState()
        {
            if (!state.IsActive) return;

            var id = state_manager_data.PopStateID();
            if(id == default) id = state_manager_data.GetNowStateID();
            switch (id)
            {
                case TitleGameStateID.BeginTitle:
                {
                    state.Exit(state_manager_data);
                    id = state_manager_data.PopStateID();
                    if(id == default) id = state_manager_data.GetNowStateID();
                    state = FactoryState(id);
                    if (state == null)
                    {
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case TitleGameStateID.TitleStartAnim:
                {
                    state.Exit(state_manager_data);
                    id = state_manager_data.PopStateID();
                    if(id == default) id = state_manager_data.GetNowStateID();
                    state = FactoryState(id);
                    if (state == null)
                    {
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case TitleGameStateID.TitleStartIdle:
                {
                    state.Exit(state_manager_data);
                    id = state_manager_data.PopStateID();
                    if(id == default) id = state_manager_data.GetNowStateID();
                    state = FactoryState(id);
                    if (state == null)
                    {
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case TitleGameStateID.TitleSelectStartAnim:
                {
                    state.Exit(state_manager_data);
                    id = state_manager_data.PopStateID();
                    if(id == default) id = state_manager_data.GetNowStateID();
                    state = FactoryState(id);
                    if (state == null)
                    {
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case TitleGameStateID.TitleSelectIdle:
                {
                    state.Exit(state_manager_data);
                    id = state_manager_data.PopStateID();
                    if(id == default) id = state_manager_data.GetNowStateID();
                    state = FactoryState(id);
                    if (state == null)
                    {
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case TitleGameStateID.InitGame:
                {
                    state.Exit(state_manager_data);
                    id = state_manager_data.PopStateID();
                    if(id == default) id = state_manager_data.GetNowStateID();
                    state = FactoryState(id);
                    if (state == null)
                    {
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case TitleGameStateID.LoadGame:
                {
                    state.Exit(state_manager_data);
                    id = state_manager_data.PopStateID();
                    if(id == default) id = state_manager_data.GetNowStateID();
                    state = FactoryState(id);
                    if (state == null)
                    {
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case TitleGameStateID.BackTitleStartIdle:
                {
                    state.Exit(state_manager_data);
                    id = state_manager_data.PopStateID();
                    if(id == default) id = state_manager_data.GetNowStateID();
                    state = FactoryState(id);
                    if (state == null)
                    {
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case TitleGameStateID.ExitGame:
                {
                    state.Exit(state_manager_data);
                    id = state_manager_data.PopStateID();
                    if(id == default) id = state_manager_data.GetNowStateID();
                    state = FactoryState(id);
                    if (state == null)
                    {
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case TitleGameStateID.OptionGame:
                {
                    state.Exit(state_manager_data);
                    id = state_manager_data.PopStateID();
                    if(id == default) id = state_manager_data.GetNowStateID();
                    state = FactoryState(id);
                    if (state == null)
                    {
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case TitleGameStateID.LicenseShow:
                {
                    state.Exit(state_manager_data);
                    id = state_manager_data.PopStateID();
                    if(id == default) id = state_manager_data.GetNowStateID();
                    state = FactoryState(id);
                    if (state == null)
                    {
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case TitleGameStateID.FadeIn:
                {
                    state.Exit(state_manager_data);
                    id = state_manager_data.PopStateID();
                    if(id == default) id = state_manager_data.GetNowStateID();
                    state = FactoryState(id);
                    if (state == null)
                    {
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case TitleGameStateID.FadeOut:
                {
                    state.Exit(state_manager_data);
                    id = state_manager_data.PopStateID();
                    if(id == default) id = state_manager_data.GetNowStateID();
                    state = FactoryState(id);
                    if (state == null)
                    {
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case TitleGameStateID.BeginTitle01:
                {
                    state.Exit(state_manager_data);
                    var next_id = TitleGameStateID.FadeOut13;
                    state_manager_data.ChangeStateNowID(next_id);
                    state_manager_data.PushStateID(TitleGameStateID.FadeOut);
                    next_id = state_manager_data.PopStateID();
                    state = FactoryState(next_id);
                    if (state == null)
                    {
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case TitleGameStateID.TitleStartAnim02:
                {
                    state.Exit(state_manager_data);
                    var next_id = TitleGameStateID.TitleStartIdle03;
                    state_manager_data.ChangeStateNowID(next_id);
                    state = FactoryState(next_id);
                    if (state == null)
                    {
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case TitleGameStateID.TitleStartIdle03:
                {
                    state.Exit(state_manager_data);
                    var next_id = TitleGameStateID.TitleSelectStartAnim04;
                    state_manager_data.ChangeStateNowID(next_id);
                    state = FactoryState(next_id);
                    if (state == null)
                    {
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case TitleGameStateID.TitleSelectStartAnim04:
                {
                    state.Exit(state_manager_data);
                    var next_id = TitleGameStateID.TitleSelectIdle05;
                    state_manager_data.ChangeStateNowID(next_id);
                    state = FactoryState(next_id);
                    if (state == null)
                    {
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case TitleGameStateID.TitleSelectIdle05:
                {
                    state.Exit(state_manager_data);
                   var next_id = state.BranchNextState(state_manager_data);
                    state_manager_data.ChangeStateNowID(next_id);
                    if (next_id == TitleGameStateID.None)
                    {
                        is_finish = true;
                        return;
                    }
                    state = FactoryState(next_id);
                    if (state == null)
                    {
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case TitleGameStateID.InitGame06:
                {
                    state.Exit(state_manager_data);
                    var next_id = TitleGameStateID.FadeIn12;
                    state_manager_data.ChangeStateNowID(next_id);
                    state = FactoryState(next_id);
                    if (state == null)
                    {
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case TitleGameStateID.LoadGame07:
                {
                    state.Exit(state_manager_data);
                    var next_id = TitleGameStateID.FadeIn12;
                    state_manager_data.ChangeStateNowID(next_id);
                    state = FactoryState(next_id);
                    if (state == null)
                    {
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case TitleGameStateID.BackTitleStartIdle08:
                {
                    state.Exit(state_manager_data);
                    var next_id = TitleGameStateID.TitleStartAnim02;
                    state_manager_data.ChangeStateNowID(next_id);
                    state = FactoryState(next_id);
                    if (state == null)
                    {
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case TitleGameStateID.ExitGame09:
                {
                    state.Exit(state_manager_data);
                    var next_id = TitleGameStateID.FadeIn12;
                    state_manager_data.ChangeStateNowID(next_id);
                    state = FactoryState(next_id);
                    if (state == null)
                    {
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case TitleGameStateID.OptionGame10:
                {
                    state.Exit(state_manager_data);
                    var next_id = TitleGameStateID.TitleSelectIdle05;
                    state_manager_data.ChangeStateNowID(next_id);
                    state = FactoryState(next_id);
                    if (state == null)
                    {
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case TitleGameStateID.LicenseShow11:
                {
                    state.Exit(state_manager_data);
                    var next_id = TitleGameStateID.TitleSelectIdle05;
                    state_manager_data.ChangeStateNowID(next_id);
                    state = FactoryState(next_id);
                    if (state == null)
                    {
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
                case TitleGameStateID.FadeIn12:
                {
                    state.Exit(state_manager_data);
                    is_finish = true;
                    return;
                }
                case TitleGameStateID.FadeOut13:
                {
                    state.Exit(state_manager_data);
                    var next_id = TitleGameStateID.TitleStartAnim02;
                    state_manager_data.ChangeStateNowID(next_id);
                    state = FactoryState(next_id);
                    if (state == null)
                    {
                        is_finish = true;
                        return;
                    }
                    state.Enter(state_manager_data);
                    return;
                }
            }
        }

        public override BaseTitleGameState FactoryState(TitleGameStateID state_id)
        {
            switch (state_id)
            {
                case TitleGameStateID.BeginTitle: return new TitleGameBeginTitleState();
                case TitleGameStateID.TitleStartAnim: return new TitleGameTitleStartAnimState();
                case TitleGameStateID.TitleStartIdle: return new TitleGameTitleStartIdleState();
                case TitleGameStateID.TitleSelectStartAnim: return new TitleGameTitleSelectStartAnimState();
                case TitleGameStateID.TitleSelectIdle: return new TitleGameTitleSelectIdleState();
                case TitleGameStateID.InitGame: return new TitleGameInitGameState();
                case TitleGameStateID.LoadGame: return new TitleGameLoadGameState();
                case TitleGameStateID.BackTitleStartIdle: return new TitleGameBackTitleStartIdleState();
                case TitleGameStateID.ExitGame: return new TitleGameExitGameState();
                case TitleGameStateID.OptionGame: return new TitleGameOptionGameState();
                case TitleGameStateID.LicenseShow: return new TitleGameLicenseShowState();
                case TitleGameStateID.FadeIn: return new TitleGameFadeInState();
                case TitleGameStateID.FadeOut: return new TitleGameFadeOutState();
                case TitleGameStateID.BeginTitle01: return new TitleGameBeginTitleState();
                case TitleGameStateID.TitleStartAnim02: return new TitleGameTitleStartAnimState();
                case TitleGameStateID.TitleStartIdle03: return new TitleGameTitleStartIdleState();
                case TitleGameStateID.TitleSelectStartAnim04: return new TitleGameTitleSelectStartAnimState();
                case TitleGameStateID.TitleSelectIdle05: return new TitleGameTitleSelectIdleState();
                case TitleGameStateID.InitGame06: return new TitleGameInitGameState();
                case TitleGameStateID.LoadGame07: return new TitleGameLoadGameState();
                case TitleGameStateID.BackTitleStartIdle08: return new TitleGameBackTitleStartIdleState();
                case TitleGameStateID.ExitGame09: return new TitleGameExitGameState();
                case TitleGameStateID.OptionGame10: return new TitleGameOptionGameState();
                case TitleGameStateID.LicenseShow11: return new TitleGameLicenseShowState();
                case TitleGameStateID.FadeIn12: return new TitleGameFadeInState();
                case TitleGameStateID.FadeOut13: return new TitleGameFadeOutState();
                default: return null;
            }
        }
    }
}

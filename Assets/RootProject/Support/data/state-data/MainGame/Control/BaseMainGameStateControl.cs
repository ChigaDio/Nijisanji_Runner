using System;
using UnityEngine;
using GameCore.States.ID;
using GameCore.States.Managers;
using GameCore.States;

namespace GameCore.States.Control
{
    public abstract class BaseMainGameStateControl
        : BaseStateControl<MainGameStateID, MainGameStateManagerData, BaseMainGameState>
    {
        protected override MainGameStateID GetInitStartID()
        {
            return MainGameStateID.BeginGame01;
        }

        public override void BranchState()
        {
            if (!state.IsActive) return;

            var id = state_manager_data.PopStateID();
            if(id == default) id = state_manager_data.GetNowStateID();
            switch (id)
            {
                case MainGameStateID.BeginGame:
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
                case MainGameStateID.TitleGame:
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
                case MainGameStateID.PlayGame:
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
                case MainGameStateID.ExitGame:
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
                case MainGameStateID.BeginGame01:
                {
                    state.Exit(state_manager_data);
                    var next_id = MainGameStateID.TitleGame02;
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
                case MainGameStateID.TitleGame02:
                {
                    state.Exit(state_manager_data);
                   var next_id = state.BranchNextState(state_manager_data);
                    state_manager_data.ChangeStateNowID(next_id);
                    if (next_id == MainGameStateID.None)
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
                case MainGameStateID.PlayGame03:
                {
                    state.Exit(state_manager_data);
                   var next_id = state.BranchNextState(state_manager_data);
                    state_manager_data.ChangeStateNowID(next_id);
                    if (next_id == MainGameStateID.None)
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
                case MainGameStateID.ExitGame04:
                {
                    state.Exit(state_manager_data);
                    is_finish = true;
                    return;
                }
            }
        }

        public override BaseMainGameState FactoryState(MainGameStateID state_id)
        {
            switch (state_id)
            {
                case MainGameStateID.BeginGame: return new MainGameBeginGameState();
                case MainGameStateID.TitleGame: return new MainGameTitleGameState();
                case MainGameStateID.PlayGame: return new MainGamePlayGameState();
                case MainGameStateID.ExitGame: return new MainGameExitGameState();
                case MainGameStateID.BeginGame01: return new MainGameBeginGameState();
                case MainGameStateID.TitleGame02: return new MainGameTitleGameState();
                case MainGameStateID.PlayGame03: return new MainGamePlayGameState();
                case MainGameStateID.ExitGame04: return new MainGameExitGameState();
                default: return null;
            }
        }
    }
}

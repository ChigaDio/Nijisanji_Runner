namespace GameCore.States.Branch
{
    public abstract class BaseDetailStateBranch<T, E, F>
        where T : Enum
        where E : GameCore.States.Managers.BaseStateManagerData<T>
        where F : GameCore.States.BaseState<E>
    {
        public abstract T ConditionsBranch(E state_manager_data, F state);
    }
}

namespace GameCore.States
{
    public class BaseState<T> where T : BaseStateManagerData
    {
        private bool is_active = true;
        public bool IsActive => is_active;

        protected void IsActiveOff(T state_manager_data)
        {
            is_active = false;
        }

        public abstract void Enter(T state_manager_data);
        public abstract void Update(T state_manager_data);
        public abstract void Exit(T state_manager_data);
        public virtual T BranchNextState(T state_manager_data)
        {
            return default;
        }

    }
}

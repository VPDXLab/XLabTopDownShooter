using System;

namespace Entities.Enemies
{
    public sealed class EnemyStateMachine
    {
        public event Action<EnemyState, EnemyState> StateChanged;
        
        public EnemyState currentState { get; private set; }

        public EnemyStateMachine()
        {
            currentState = EnemyState.Idle;
        }

        public bool ChangeState(EnemyState newState)
        {
            if (currentState is EnemyState.Dead || currentState == newState)
            {
                return false;
            }
            
            var previousState = currentState;
            currentState = newState;
            StateChanged?.Invoke(previousState, newState);
            
            return true;
        }
    }
}


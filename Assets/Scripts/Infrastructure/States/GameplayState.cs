using Cameras;
using Players;
using UnityEngine.InputSystem;

namespace Infrastructure.States
{
    public class GameplayState : IState
    {
        private readonly StateMachine m_stateMachine;
        private readonly CameraFollow m_cameraFollow;
        
        private PlayerController m_playerController;

        public GameplayState(
            StateMachine stateMachine,
            CameraFollow cameraFollow)
        {
            m_cameraFollow = cameraFollow;
            m_stateMachine = stateMachine;
        }
        
        public void Enter()
        {
            m_playerController = ServiceLocator.Resolve<IPlayerFactory>().Create();
            
            m_cameraFollow.SetTarget(m_playerController.transform);
            m_playerController.Health.Died += OnDied;
        }
        
        public void Update()
        {
            if (Keyboard.current[Key.Escape].wasPressedThisFrame)
            {
                m_stateMachine.ChangedState<PauseMenuState>();
            }
        }

        public void Exit()
        {
            m_playerController.Health.Died -= OnDied;
            m_playerController = null;
        }

        private void OnDied() =>
            m_stateMachine.ChangedState<DeadState>();
    }
}
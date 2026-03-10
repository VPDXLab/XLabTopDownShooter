using Cameras;
using Entities.Enemies;
using Markers;
using Players;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Infrastructure.States
{
    public class GameplayState : IState
    {
        private readonly StateMachine m_stateMachine;
        private readonly CameraFollow m_cameraFollow;
        private readonly SpawnerEnemy m_spawnerEnemy;
        private readonly AimLineMarker m_aimLineMarker;
        private readonly TargetMarkerObserver m_targetMarkerObserver;
        
        private PlayerController m_playerController;

        public GameplayState(
            StateMachine stateMachine,
            CameraFollow cameraFollow,
            SpawnerEnemy spawnerEnemy,
            AimLineMarker aimLineMarker,
            TargetMarkerObserver targetMarkerObserver)
        {
            m_cameraFollow = cameraFollow;
            m_spawnerEnemy = spawnerEnemy;
            m_stateMachine = stateMachine;
            m_aimLineMarker = aimLineMarker;
            m_targetMarkerObserver = targetMarkerObserver;
        }
        
        public void Enter()
        {
            var playerPosition = ServiceLocator.Resolve<PlayerSpawnPoint>();
            ServiceLocator.Resolve<IPlayerFactorySettings>().position = playerPosition.transform.position;
            m_playerController = ServiceLocator.Resolve<IPlayerFactory>().Create();
            
            m_cameraFollow.SetTarget(m_playerController.transform);
            m_aimLineMarker.Initialize(m_playerController.transform);
            m_targetMarkerObserver.Initialize(m_playerController.GetComponent<PlayerMovement>());
            
            m_spawnerEnemy.Spawn();
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
        }

        private void OnDied() =>
            m_stateMachine.ChangedState<DeadState>();
    }
}
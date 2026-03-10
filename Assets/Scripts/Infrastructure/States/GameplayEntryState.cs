using Markers;
using Players;
using Entities.Enemies;

namespace Infrastructure.States
{
    public class GameplayEntryState : IState
    {
        private PlayerController m_playerController;
        private readonly SpawnerEnemy m_spawnerEnemy;
        private readonly StateMachine m_stateMachine;
        private readonly AimLineMarker m_aimLineMarker;
        private readonly TargetMarkerObserver m_targetMarkerObserver;

        public GameplayEntryState(
            StateMachine stateMachine,
            SpawnerEnemy spawnerEnemy,
            AimLineMarker aimLineMarker,
            TargetMarkerObserver targetMarkerObserver)
        {
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
            
            m_aimLineMarker.Initialize(m_playerController.transform);
            m_targetMarkerObserver.Initialize(m_playerController.GetComponent<PlayerMovement>());
            
            m_spawnerEnemy.Spawn();
            m_stateMachine.ChangedState<GameplayState>();
        }

        public void Exit() { }
    }
}
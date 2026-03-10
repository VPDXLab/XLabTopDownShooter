using Cameras;
using UI;
using UnityEngine;
using Entities.Enemies;
using Infrastructure.States;
using Markers;

namespace Infrastructure
{
    public class BootstrapGameplay : MonoBehaviour
    {
        [SerializeField] private TargetMarkerObserver m_targetMarkerObserver;
        [SerializeField] private BootstrapState m_bootstrapState;
        [SerializeField] private DeadMenuView m_deadMenuView;
        [SerializeField] private SpawnerEnemy m_enemySpawner;
        [SerializeField] private AimLineMarker m_aimLineMarker;
        [SerializeField] private CameraFollow m_cameraFollow;
        [SerializeField] private PauseMenuView m_pauseMenuView;
        
        private StateMachine m_stateMachine;
        
        private void Awake()
        {
            m_stateMachine = new StateMachine();
            m_bootstrapState.Initialize(m_stateMachine);

            m_stateMachine.Initialize(
                m_bootstrapState,
                new PauseMenuState(m_stateMachine, m_pauseMenuView),
                new DeadState(m_stateMachine, m_deadMenuView),
                new GameplayState(m_stateMachine, m_cameraFollow),
                new GameplayExitState(m_enemySpawner),
                new GameplayEntryState(
                    m_stateMachine,
                    m_enemySpawner,
                    m_aimLineMarker,
                    m_targetMarkerObserver));
            
            m_stateMachine.ChangedState<BootstrapState>();
        }

        private void Update() =>
            m_stateMachine.Update();
    }
}
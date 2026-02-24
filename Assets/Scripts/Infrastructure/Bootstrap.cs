using Cameras;
using UI;
using UnityEngine;
using Entities.Enemies;
using Infrastructure.States;
using Markers;
using Players;

namespace Infrastructure
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private TargetMarkerObserver m_targetMarkerObserver;
        [SerializeField] private BootstrapState m_bootstrapState;
        [SerializeField] private DeadMenuView m_deadMenuView;
        [SerializeField] private SpawnerEnemy m_enemySpawner;
        [SerializeField] private AimLineMarker m_aimLineMarker;
        [SerializeField] private CameraFollow m_cameraFollow;
        
        private void Awake()
        {
            var stateMachine = new StateMachine();
            m_bootstrapState.Initialize(stateMachine);
            
            stateMachine.Initialize(
                m_bootstrapState,
                new PauseMenuState(stateMachine),
                new DeadState(stateMachine, m_deadMenuView),
                new GameplayState(
                    stateMachine,
                    m_cameraFollow,
                    m_enemySpawner,
                    m_aimLineMarker,
                    m_targetMarkerObserver));
            
            stateMachine.ChangedState<BootstrapState>();
        }
    }
}
using UI;
using UnityEngine;
using Entities.Enemies;
using Infrastructure.States;

namespace Infrastructure
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private MainMenuView m_mainMenuView;
        [SerializeField] private SpawnerEnemy m_enemySpawner; 
        
        private void Awake()
        {
            var stateMachine = new StateMachine();
            
            stateMachine.Initialize(
                new MainMenuState(stateMachine, m_mainMenuView),
                new PauseMenuState(stateMachine),
                new DeadState(stateMachine),
                new GameplayState(stateMachine, m_enemySpawner));
            
            stateMachine.ChangedState<MainMenuState>();
        }
    }
}
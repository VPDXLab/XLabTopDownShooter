using System;
using System.Collections.Generic;
using Entities.Enemies;
using UI;
using UnityEngine;

namespace Infrastructure.States
{
    public class StateMachine
    {
        private IState m_state;
        private Dictionary<Type, IState> m_states = new();

        public void Initialize(params IState[] states)
        {
            if (m_states.Count > 0) return;
            
            foreach (var state in states)
            {
                m_states.Add(state.GetType(), state);
            }
        }
        
        public void ChangedState<T>()
            where T : IState
        {
            m_state?.Exit();
            {
                m_state = m_states[typeof(T)];
            }
            m_state.Enter();
        }
    }

    public interface IState
    {
        public void Enter();
        
        public void Exit();
    }

    public class MainMenuState : IState
    {
        private readonly StateMachine m_stateMachine;
        private readonly MainMenuView m_mainMenuView;

        public MainMenuState(
            StateMachine stateMachine,
            MainMenuView mainMenuView)
        {
            m_stateMachine = stateMachine;
            m_mainMenuView = mainMenuView;
            
            m_mainMenuView.gameObject.SetActive(false);
        }
        
        public void Enter()
        {
            m_mainMenuView.gameObject.SetActive(true);
            m_mainMenuView.PlayClicked += OnPlayClicked;
            m_mainMenuView.ExitClicked += OnExitClicked;
        }

        public void Exit()
        {
            m_mainMenuView.PlayClicked -= OnPlayClicked;
            m_mainMenuView.ExitClicked -= OnExitClicked;
            m_mainMenuView.gameObject.SetActive(false);
        }
        
        private void OnPlayClicked() =>
            m_stateMachine.ChangedState<GameplayState>();
        
        private void OnExitClicked()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.ExitPlaymode();
#endif
            Application.Quit();
        }
    }
    
    public class PauseMenuState : IState
    {
        private readonly StateMachine m_stateMachine;

        public PauseMenuState(StateMachine stateMachine)
        {
            m_stateMachine = stateMachine;
        }
        
        public void Enter() =>  throw new NotImplementedException();

        public void Exit() =>  throw new NotImplementedException();
    }
    
    public class GameplayState : IState
    {
        private readonly StateMachine m_stateMachine;
        private readonly SpawnerEnemy m_spawnerEnemy;

        public GameplayState(
            StateMachine stateMachine,
            SpawnerEnemy spawnerEnemy)
        {
            m_spawnerEnemy = spawnerEnemy;
            m_stateMachine = stateMachine;
        }
        
        public void Enter()
        {
            m_spawnerEnemy.Spawn();
        }

        public void Exit() =>  throw new NotImplementedException();
    }
    
    public class DeadState : IState
    {
        private readonly StateMachine m_stateMachine;

        public DeadState(StateMachine stateMachine)
        {
            m_stateMachine = stateMachine;
        }
        
        public void Enter() =>  throw new NotImplementedException();

        public void Exit() =>  throw new NotImplementedException();
    }
}

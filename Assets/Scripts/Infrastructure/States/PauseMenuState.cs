using UI;
using UnityEngine;

namespace Infrastructure.States
{
    public class PauseMenuState : IState
    {
        private readonly Loading m_loading;
        private readonly StateMachine m_stateMachine;
        private readonly PauseMenuView m_pauseMenuView;

        public PauseMenuState(
            StateMachine stateMachine,
            PauseMenuView pauseMenuView)
        {
            m_stateMachine = stateMachine;
            m_pauseMenuView = pauseMenuView;
            m_loading = ServiceLocator.Resolve<Loading>();
        }

        public void Enter()
        {
            Time.timeScale = 0;
            m_pauseMenuView.gameObject.SetActive(true);
            m_pauseMenuView.ContinueClicked += OnContinueClicked;
            m_pauseMenuView.MainMenuClicked += OnPauseMenuClicked;
        }

        public void Exit()
        {
            Time.timeScale = 1;
            m_pauseMenuView.gameObject.SetActive(false);
            m_pauseMenuView.ContinueClicked -= OnContinueClicked;
            m_pauseMenuView.MainMenuClicked -= OnPauseMenuClicked;
        }

        private void OnContinueClicked() =>
            m_stateMachine.ChangedState<GameplayState>();

        private void OnPauseMenuClicked()
        {
            Exit();
            m_loading.LoadScene(GlobalConstants.Scenes.Main);
        }
    }
}
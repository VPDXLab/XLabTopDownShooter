using Entities.Enemies;

namespace Infrastructure.States
{
    public class GameplayExitState : IState
    {
        private readonly SpawnerEnemy m_spawnerEnemy;

        public GameplayExitState(SpawnerEnemy spawnerEnemy)
        {
            m_spawnerEnemy = spawnerEnemy;
        }
        
        public void Enter()
        {
            var loading = ServiceLocator.Resolve<Loading>();
            m_spawnerEnemy.DespawnAll();
            
            loading.LoadScene(GlobalConstants.Scenes.Main);
        }

        public void Exit() { }
    }
}
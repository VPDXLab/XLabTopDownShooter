using System;
using UnityEngine;
using Entities.Enemies.Data;

namespace Entities.Enemies
{
    public class Enemy : MonoBehaviour
    {
        public event Action<Enemy> Died;
        
        [SerializeField] private HealthComponent m_health;
        [SerializeField] private EnemyMovement m_movement;
        [SerializeField] private EnemyAttackController m_attackController;
        
        private EnemyData m_data;
        private Transform m_playerTransform;
        private EnemyStateMachine m_stateMachine;

        private void Awake()
        {
            m_stateMachine = new EnemyStateMachine();
        }

        private void OnEnable()
        {
            m_health.Died += OnDied;
            m_stateMachine.StateChanged += OnStateChanged;
        }

        private void OnDisable()
        {
            m_health.Died -= OnDied;
            m_stateMachine.StateChanged -= OnStateChanged;
        }

        public void Initialize(EnemyData data, Transform playerTransform)
        {
            m_data = data;
            
            m_playerTransform = playerTransform;
            m_health.Initialize(data.health);
            m_movement.Initialize(data.speed, playerTransform);
            m_attackController.Initialize(data.spell, data.attackTime, playerTransform);
            
            if (data.enemyType == AttackEnemyType.Melee)
            {
                m_stateMachine.ChangeState(EnemyState.Move);
            }
        }

        private void Update()
        {
            if (m_stateMachine.currentState is EnemyState.Dead || m_data is null)
            {
                return;
            }

            UpdateStateMachine();
        }

        private void UpdateStateMachine()
        {
            var isInAttackRange = IsInRange();
            Debug.Log(isInAttackRange);

            switch (m_stateMachine.currentState)
            {
                case EnemyState.Idle: HandleIdleState(isInAttackRange); break;
                case EnemyState.Move: HandleMoveState(isInAttackRange); break;
                case EnemyState.Attack: HandleAttackState(isInAttackRange); break;
            }
        }

        private void HandleIdleState(bool isInAttackRange)
        {
            if (m_data.enemyType == AttackEnemyType.Range && isInAttackRange)
            {
                m_stateMachine.ChangeState(EnemyState.Attack);
            }
        }

        private void HandleMoveState(bool isInAttackRange)
        {
            if (isInAttackRange)
            {
                m_stateMachine.ChangeState(EnemyState.Attack);
            }
        }

        private void HandleAttackState(bool isInAttackRange)
        {
            m_attackController.TryAttack();
            
            if (m_data.enemyType == AttackEnemyType.Melee && !isInAttackRange)
            {
                m_stateMachine.ChangeState(EnemyState.Move);
            }
        }
        
        private bool IsInRange()
        {
            if (!m_playerTransform) return false;
            
            var distance = Vector3.Distance(transform.position, m_playerTransform.position);
            return distance <= m_data.attackRange;
        }

        private void OnStateChanged(EnemyState previousState, EnemyState newState)
        {
            if (previousState == EnemyState.Move)
            {
                m_movement.StopMoving();
            }
            
            if (newState == EnemyState.Move)
            {
                m_movement.StartMoving();
            }
        }

        private void OnDied()
        {
            m_stateMachine.ChangeState(EnemyState.Dead);
            Died?.Invoke(this);
        }
    }
}
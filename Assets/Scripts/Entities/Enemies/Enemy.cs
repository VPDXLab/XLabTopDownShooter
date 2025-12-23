using System;
using Entities.Enemies.Data;
using UnityEngine;

namespace Entities.Enemies
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private EnemyData m_enemyData;
        [SerializeField] private HealthComponent m_health;
        
        private EnemyData m_data;
        
        // TODO Add HealthComponent
        // TODO Add Movement
        // TODO Add AttackComponent

        private void Awake()
        {
            Initialize(m_enemyData);
        }

        private void OnEnable()
        {
            m_health.ValueChanged += () =>
            {
                Debug.Log($"Health Changed: {m_health.Value}");
            };

            m_health.Died += OnDied;
        }

        private void OnDisable()
        {
            m_health.Died -= OnDied;
        }

        public void Initialize(EnemyData data)
        {
            m_data = data;
            m_health.Initialize(data.health);
        }

        private void OnDied()
        {
            Debug.Log("Enemy Died");
            Destroy(gameObject);
        }
    }
}
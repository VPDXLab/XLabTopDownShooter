using System;
using Magic.Effects;
using UnityEngine;

namespace Entities
{
    public class HealthComponent : MonoBehaviour, IHealth, IEffectable
    {
        public event Action Died;
        public event Action ValueChanged;

        private float m_value;
        private bool m_initialized;
        
        public float value
        {
            get => m_value;
            private set
            {
                if (Mathf.Approximately(m_value, value))
                {
                    return;
                }
                
                m_value = value < 0 ? 0 : value;
                ValueChanged?.Invoke();

                if (m_value is 0)
                {
                    Died?.Invoke();
                }
            }
        }

        public void Initialize(float value)
        {
            if (m_initialized)
            {
                throw new InvalidOperationException("HealthComponent is already initialized");
            }
            
            m_value = value;
            m_initialized = true;
        }
        
        public void Heal(float heal)
        {
            if (heal < 0)
                throw new ArgumentOutOfRangeException(nameof(heal), heal, "Heal cannot be negative");
            
            value += heal;
        }

        public void TakeDamage(float damage)
        {
            if (damage < 0)
                throw new ArgumentOutOfRangeException(nameof(damage), damage, "Heal cannot be negative");
            
            value -= damage;
        }
    }
}
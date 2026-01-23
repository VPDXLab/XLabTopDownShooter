using System;
using Entities;
using Magic.Buffs.Base;
using UnityEngine;

namespace Magic.Buffs.Impls
{
    [Serializable]
    public sealed class PoisonDebuff : TimedBuff
    {
        [SerializeField] [Min(0)] private float m_interval = 1;
        [SerializeField] [Min(0)] private float m_damagedPerSeconds = 2f;

        [NonSerialized] private float m_timer;
        private IHealth m_health;

        public PoisonDebuff() { }
        
        public PoisonDebuff(
            string id, 
            Sprite icon,
            BuffType type,
            float duration,
            float interval, 
            float damagedPerSeconds) 
            : base(id, icon, type, duration)
        {
            m_interval = interval;
            m_damagedPerSeconds = damagedPerSeconds;
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            m_health = container.GetComponent<IHealth>();
        }

        protected override void OnDeinitializing()
        {
            m_timer = 0;
            m_health = null;
            base.OnDeinitializing();
        }

        protected override void OnUpdated(float deltaTime)
        {
            if (m_health is null)
            {
                Deinitialize();
                return;
            }
            
            if (m_timer < m_interval)
            {
                m_timer += deltaTime;
            }
            else
            {
                m_timer = 0;
                m_health.TakeDamage(m_damagedPerSeconds);
            }
        }
        
        public override IBuff Clone() =>
            new PoisonDebuff(Id, Icon, Type, duration, m_interval, m_damagedPerSeconds);
    }
}
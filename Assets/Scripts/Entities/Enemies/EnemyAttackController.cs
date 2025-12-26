using UnityEngine;
using Magic.Systems;
using Magic.Spells.Data;

namespace Entities.Enemies
{
    public class EnemyAttackController : MonoBehaviour
    {
        private Transform m_target;
        private BaseSpellData m_spell;
        private SpellCaster m_spellCaster;
        
        private float m_attackTime;
        private float m_cooldownTimer;
        private bool m_initialized;

        public void Initialize(BaseSpellData spell, float attackTime, Transform target)
        {
            m_spell = spell;
            m_target = target;
            m_cooldownTimer = 0f;
            m_initialized = true;
            m_attackTime = attackTime;
            m_spellCaster = new SpellCaster(transform);
        }

        private void Update()
        {
            if (!m_initialized) return;
            
            if (m_cooldownTimer > 0f)
            {
                m_cooldownTimer -= Time.deltaTime;
            }
        }

        public bool TryAttack()
        {
            if (!m_initialized || !m_target || !m_spell)
            {
                return false;
            }

            if (m_cooldownTimer > 0f)
            {
                return false;
            }

            m_spellCaster.Cast(m_spell, m_target.position);
            m_cooldownTimer = m_attackTime;
            return true;
        }
    }
}


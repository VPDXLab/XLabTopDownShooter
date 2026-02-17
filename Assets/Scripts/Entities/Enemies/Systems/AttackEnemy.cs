using System.Collections.Generic;
using System.Linq;
using Entities.Enemies.Data;
using Magic.Spells.Data;
using UnityEngine;
using Magic.Systems;

namespace Entities.Enemies.Systems
{
    public sealed class AttackEnemy : MonoBehaviour
    {
        private Transform m_target;
        private SpellCaster m_spellCaster;
        private IReadOnlyList<SpellEnemyData> m_spells;

        private float m_attackTime;
        private bool m_isInitialized;
        private float m_cooldownTimer;
        
        private int m_count;
        private int m_maxCount;
        private BaseSpellData m_defaultSpell;

        public void Initialize(
            BaseSpellData defaultSpell,
            IReadOnlyList<SpellEnemyData> spells,
            float attackTime, 
            Transform target)
        {
            if (m_isInitialized)
            {
                return;
            }

            m_target = target;
            m_attackTime = attackTime;
            m_defaultSpell = defaultSpell;
            m_spells = spells.OrderBy(spell => spell.count).ToArray();
            m_spellCaster = new SpellCaster(transform, true);

            m_maxCount = spells.LastOrDefault().count;
            m_isInitialized = true;
        }

        private void Update()
        {
            if (!m_isInitialized)
            {
                return;
            }

            if (m_cooldownTimer > 0)
            {
                m_cooldownTimer -= Time.deltaTime;
            }
        }

        public bool TryAttack()
        {
            if (!m_isInitialized || !m_target)
            {
                return false;
            }

            if (m_cooldownTimer > 0)
            {
                return false;
            }
            
            m_count++;
            var spell = m_spells.FirstOrDefault(spell => spell.count == m_count);

            if (spell.spell is null)
            {
                m_spellCaster.Cast(m_defaultSpell, m_target.position);
            }
            else
            {
                m_spellCaster.Cast(spell.spell, m_target.position);
            }

            if (m_count == m_maxCount)
                m_count = 0;
            
            m_cooldownTimer = m_attackTime;
            return true;
        }
    }
}
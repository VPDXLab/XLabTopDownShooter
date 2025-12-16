using System;
using Inputs;
using Magic.Data;
using UnityEngine;
using Magic.Elements;
using System.Collections;
using System.Collections.Generic;

namespace Magic.Systems
{
    public class MagicSystem : MonoBehaviour
    {
        public event Action SpellCancelled;
        public event Action<MagicState> StateChanged;
        
        public event Action<IReadOnlyList<ElementType>> ElementsChanged
        {
            add => spellPreparation.ElementsChanged += value;
            remove => spellPreparation.ElementsChanged -= value;
        }
        
        [SerializeField] private MagicConfig m_magicConfig;
        [SerializeField] private MouseResolver m_mouseResolver;
        
        private MagicState m_state;
        private SpellCaster m_caster;
        private Coroutine m_cooldownCoroutine;
        private SpellPreparation m_spellPreparation;

        public MagicState state
        {
            get => m_state;
            private set
            {
                if (value != m_state)
                {
                    m_state = value;
                    StateChanged?.Invoke(value);
                }
            }
        }

        private SpellPreparation spellPreparation =>
            m_spellPreparation ??= new SpellPreparation(m_magicConfig);

        private void Awake()
        {
            m_caster = new SpellCaster(transform);
        }

        private void OnEnable() =>
            spellPreparation.OverflowOccurred += CancelSpell;

        private void OnDisable() =>
            spellPreparation.OverflowOccurred -= CancelSpell;
        
        public void AddElement(ElementType element)
        {
            if (state is MagicState.Cooldown or MagicState.Casting) return;
            
            spellPreparation.AddElement(element);
            state = MagicState.Preparing;
        }
        
        public void TryCastSpell()
        {
            if (state is not MagicState.Preparing) return;
            
            if (spellPreparation.TryGetSpell(out var spell))
            {
                state = MagicState.Casting;
                
                var cursorWorldPosition = m_mouseResolver.GetCursorWorldPosition();
                m_caster.Cast(spell, cursorWorldPosition.Value);
                
                spellPreparation.Clear();
                state = MagicState.Idle;
            }
            else
            {
                CancelSpell();
            }
        }

        private void CancelSpell()
        {
            if (state is MagicState.Preparing)
            {
                spellPreparation.Clear();
                SpellCancelled?.Invoke();

                StartCoolDown();
            }
        }

        private void StartCoolDown()
        {
            if (m_cooldownCoroutine is not null)
            {
                StopCoroutine(m_cooldownCoroutine);
            }
            
            m_cooldownCoroutine = StartCoroutine(CooldownCoroutine());
        }

        private IEnumerator CooldownCoroutine()
        {
            state = MagicState.Cooldown;
            yield return new WaitForSeconds(m_magicConfig.cancelCooldown);
            state = MagicState.Idle;

            m_cooldownCoroutine = null;
        }
    }
}

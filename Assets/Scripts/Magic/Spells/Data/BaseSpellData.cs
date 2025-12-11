using UnityEngine;
using System.Linq;
using Magic.Effects;
using Magic.Elements;
using System.Collections.Generic;

namespace Magic.Spells.Data
{
    public abstract class BaseSpellData : ScriptableObject
    {
        [SerializeField] private string _spellName;
        [SerializeField] private GameObject m_effect;
        [SerializeField] private ElementType[] m_combination;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private IEffect[] m_effects;
        
        public string spellName => _spellName;
        
        public GameObject effect => m_effect;
        
        public IReadOnlyList<IEffect> effects => m_effects;
        
        public IReadOnlyList<ElementType> combination => m_combination;
        
        protected virtual void OnValidate()
        {
            if (m_combination?.Length > 3)
            {
                m_combination = m_combination.Take(3).ToArray();
            }
        }
    }
}

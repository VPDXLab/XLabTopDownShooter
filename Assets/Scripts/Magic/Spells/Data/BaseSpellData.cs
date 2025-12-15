using UnityEngine;
using System.Linq;
using Magic.Effects;
using Magic.Elements;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace Magic.Spells.Data
{
    public abstract class BaseSpellData : ScriptableObject
    {
        [SerializeField] private string _spellName;
        [FormerlySerializedAs("m_effect")] [SerializeField] private GameObject _mVisualEffect;
        [SerializeField] private ElementType[] m_combination;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private IEffect[] m_effects;
        
        public string spellName => _spellName;
        
        public GameObject visualEffect => _mVisualEffect;
        
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

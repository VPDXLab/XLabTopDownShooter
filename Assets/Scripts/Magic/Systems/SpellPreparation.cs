using System;
using Magic.Data;
using Magic.Elements;
using Magic.Spells.Data;
using System.Collections.Generic;

namespace Magic.Systems
{
    public sealed class SpellPreparation
    {
        public event Action OverflowOccurred;
        public event Action<IReadOnlyList<ElementType>> ElementsChanged;
        
        private readonly int m_maxElements;
        private readonly List<ElementType> m_elements;
        private readonly SpellsDataBase m_spellsDatabase;
        
        public SpellPreparation(MagicConfig magicConfig)
        {
            m_maxElements = magicConfig.maxElements;
            m_spellsDatabase = magicConfig.SpellsDataBase;
            m_elements = new List<ElementType>(m_maxElements);
        }

        public void AddElement(ElementType elementType)
        {
            if (m_elements.Count >= m_maxElements)
            {
                Clear();
                OverflowOccurred?.Invoke();
            }
            else
            {
                m_elements.Add(elementType);
                ElementsChanged?.Invoke(m_elements);
            }
        }

        public bool TryGetSpell(out BaseSpellData spell)
        {
            spell = null;
            
            if (m_elements.Count is 0)
            {
                return false;
            }
            
            foreach (var spellData in m_spellsDatabase.spells)
            {
                if (IsMatchingCombination(spellData.combination))
                {
                    spell = spellData;
                    return true;
                }
            }
            
            return false;
        }
        
        public void Clear()
        {
            m_elements.Clear();
            ElementsChanged?.Invoke(m_elements);
        }
        
        private bool IsMatchingCombination(IReadOnlyList<ElementType> combination)
        {
            if (combination.Count != m_elements.Count)
            {
                return false;
            }
            
            for (var i = 0; i < combination.Count; i++)
            {
                if (combination[i] != m_elements[i])
                {
                    return false;
                }
            }
            
            return true;
        }
    }
}

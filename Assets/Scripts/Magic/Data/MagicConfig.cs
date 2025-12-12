using Magic.Elements;
using Magic.Spells.Data;
using UnityEngine;

namespace Magic.Data
{
    public sealed class MagicConfig : ScriptableObject
    {
        [SerializeField] private ElementsData m_elementsData;
        [SerializeField] private SpellsDatabase m_spellsDataBase;

        [SerializeField] [Min(1)] private int m_maxElements = 3;
        [SerializeField] [Min(0)] private float m_cancelCooldown = 0.3f;
        
        public ElementsData ElementsData => m_elementsData;
        
        public SpellsDatabase SpellsDataBase => m_spellsDataBase;
        
        public int maxElements => m_maxElements;
        
        public float cancelCooldown => m_cancelCooldown;
    }
}
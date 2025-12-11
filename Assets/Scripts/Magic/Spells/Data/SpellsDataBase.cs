using UnityEngine;
using System.Collections.Generic;

namespace Magic.Spells.Data
{
    public sealed class SpellsDataBase : ScriptableObject
    {
        [SerializeField] private BaseSpellData[] m_spells;
        
        public IReadOnlyList<BaseSpellData> spells => m_spells;
    }
}

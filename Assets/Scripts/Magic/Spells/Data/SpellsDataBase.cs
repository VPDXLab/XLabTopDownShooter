using UnityEngine;
using System.Collections.Generic;

namespace Magic.Spells.Data
{
    [CreateAssetMenu(fileName = "SpellsDataBase", menuName = "XLab/Magic/Spells/Spells Database")]
    public sealed class SpellsDataBase : ScriptableObject
    {
        [SerializeField] private BaseSpellData[] m_spells;
        
        public IReadOnlyList<BaseSpellData> spells => m_spells;
    }
}

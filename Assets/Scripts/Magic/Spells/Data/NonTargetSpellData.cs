using UnityEngine;

namespace Magic.Spells.Data
{
    [CreateAssetMenu(fileName = "NonTargetSpellData", menuName = "XLab/Magic/Spells/Non Target Spell")]
    public class NonTargetSpellData : BaseSpellData
    {
        [SerializeField] [Min(0)] private float m_range;
        [SerializeField] [Min(0)] private float m_duration;
        
        public float range => m_range;
        
        public float duration => m_duration;
    }
}

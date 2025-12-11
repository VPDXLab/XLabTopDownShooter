using UnityEngine;

namespace Magic.Spells.Data
{
    [CreateAssetMenu(fileName = "TargetSpellData", menuName = "XLab/Magic/Spells/Target Spell")]
    public class TargetSpellData : BaseSpellData
    {
        [SerializeField] [Min(0)] private float m_speed;
        [SerializeField] [Min(0)] private float m_damage;
        
        public float speed => m_speed;
        
        public float damage => m_damage;
    }
}

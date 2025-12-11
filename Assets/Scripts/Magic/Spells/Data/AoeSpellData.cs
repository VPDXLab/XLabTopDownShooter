using UnityEngine;

namespace Magic.Spells.Data
{
    [CreateAssetMenu(fileName = "AoeSpellData", menuName = "XLab/Magic/Spells/Aoe Spell")]
    public class AoeSpellData : BaseSpellData
    {
        [SerializeField] private float m_radius;
        
        public float radius => m_radius;
    }
}

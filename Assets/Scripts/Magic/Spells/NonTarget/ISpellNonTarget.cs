using UnityEngine;

namespace Magic.Spells.NonTarget
{
    public interface ISpellNonTarget
    {
        public void Initialize(float range, float duration, Transform source);
    }
}

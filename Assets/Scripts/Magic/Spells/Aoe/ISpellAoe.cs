using UnityEngine;
using Magic.Effects;
using System.Collections.Generic;

namespace Magic.Spells.Aoe
{
    public interface ISpellAoe
    {
        public void Initialize(Vector3 worldPosition, float radius, IReadOnlyCollection<IEffect> effects);
    }
}

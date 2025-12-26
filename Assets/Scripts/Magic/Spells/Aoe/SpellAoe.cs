using UnityEngine;
using Magic.Effects;
using Magic.Effects.Extensions;
using System.Collections.Generic;

namespace Magic.Spells.Aoe
{
    public sealed class SpellAoe : MonoBehaviour, ISpellAoe
    {
        public void Initialize(Vector3 targetPosition, float radius, IReadOnlyCollection<IEffect> effects)
        {
            foreach (var collider in Physics.OverlapSphere(targetPosition, radius))
            {
                if (collider.gameObject.layer != gameObject.layer)
                {
                    effects.ApplyEffects(collider.GetComponents<IEffectable>());
                }
            }
        }
    }
}
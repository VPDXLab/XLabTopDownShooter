using System.Collections.Generic;
using Magic.Effects;

namespace Magic.Spells.Projectiles
{
    public static class EffectsExtensions
    {
        public static void ApplyEffects(
            this IReadOnlyCollection<IEffect> effects,
            IEffectable effectable)
        {
            if (effects is null) return;
            
            foreach (var effect in effects)
            {
                effect?.Apply(effectable);
            }
        }
        
        public static void ApplyEffects(
            this IReadOnlyCollection<IEffect> effects,
            IReadOnlyCollection<IEffectable> effectables)
        {
            if (effects is null) return;
            
            foreach (var effect in effects)
            {
                foreach (var effectable in effectables)
                {
                    effect?.Apply(effectable);
                }
            }
        }
    }
}
using System.Collections.Generic;

namespace Magic.Effects.Extensions
{
    public static class EffectsExtensions
    {
        public static void ApplyEffects(this IReadOnlyCollection<IEffect> effects, IReadOnlyCollection<IEffectable> effectables)
        {
            foreach (var effectable in effectables)
            {
                foreach (var effect in effects)
                {
                    effect.Apply(effectable);
                }
            }
        }
    }
}

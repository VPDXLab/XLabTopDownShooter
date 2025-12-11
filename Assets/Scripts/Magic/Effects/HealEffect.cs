using System;
using Entities;
using UnityEngine;

namespace Magic.Effects
{
    [Serializable]
    public sealed class HealEffect : IEffect
    {
        [SerializeField] [Min(0)] private float _heal;
        
        public void Apply(IEffectable effectable)
        {
            if (effectable is IHealth health)
            {
                health.Heal(_heal);
            }
        }
    }
}

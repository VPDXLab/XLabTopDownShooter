using System;
using Entities;
using UnityEngine;

namespace Magic.Effects
{
    [Serializable]
    public sealed class AttackEffect : IEffect
    {
        [SerializeField] [Min(0)] private float _damage;
        
        public void Apply(IEffectable effectable)
        {
            if (effectable is IHealth health)
            {
                health.TakeDamage(_damage);
            }
        }
    }
}

using System;
using UnityEngine;
using Magic.Effects;
using Magic.Spells.Aoe;
using Magic.Spells.Data;
using Magic.Spells.Projectiles;
using Object = UnityEngine.Object;

namespace Magic.Systems
{
    public sealed class SpellCaster
    {
        private readonly Transform m_casterTransform;

        public SpellCaster(Transform casterTransform)
        {
            m_casterTransform = casterTransform;
        }

        public void Cast(BaseSpellData spell, Vector3 worldPosition)
        {
            if (!spell)
            {
                return;
            }
            
            switch (spell)
            {
                case SelfSpellData selfSpell: CastSelf(selfSpell); break;
                case TargetSpellData targetSpell: CastTarget(targetSpell, worldPosition); break;
                case NonTargetSpellData nonTargetSpell: CastNonTarget(nonTargetSpell); break;
                case TargetAoeSpellData targetAoeSpell: CastTargetAoe(targetAoeSpell, worldPosition); break;
                case AoeSpellData aoeSpell: CastAoe(aoeSpell, m_casterTransform.position); break;
            }
        }

        private void CastSelf(SelfSpellData selfSpell)
        {
            if (selfSpell.visualEffect)
            {
                Object.Instantiate(selfSpell.visualEffect, m_casterTransform.position, Quaternion.identity);
            }
            
            if (m_casterTransform.TryGetComponent<IEffectable>(out var effectable))
            {
                foreach (var effect in selfSpell.effects)
                {
                    effect.Apply(effectable);
                }
            }
        }

        private void CastTarget(TargetSpellData targetSpell, Vector3 worldPosition)
        {
            if (!targetSpell.visualEffect)
            {
                throw new NullReferenceException("Target spell must have visualEffect");
            }
            
            var projectile = Object.Instantiate(targetSpell.visualEffect, m_casterTransform.position, Quaternion.identity);

            var spellProjectile = 
                projectile.GetComponent<ISpellProjectile>() ??
                projectile.AddComponent<SpellProjectile>();

            spellProjectile.Initialize(worldPosition, targetSpell.speed, targetSpell.effects);
        }
        
        private void CastNonTarget(NonTargetSpellData nonTargetSpell)
        {
            if (!nonTargetSpell.visualEffect)
            {
                throw new NullReferenceException("NonTarget spell must have visualEffect");
            }
            
            // if (!spell.visualEffect)
            //     return;
            //
            // var direction = (cursorPosition - m_casterTransform.position).normalized;
            // direction.y = 0;
            //
            // var spawnPosition = m_casterTransform.position + direction * spell.range;
            //
            // var effectObject = GameObject.Instantiate(spell.visualEffect, spawnPosition, Quaternion.identity);
            //
            // // Применяем эффекты к области
            // ApplyEffectsInArea(effectObject.transform.position, spell);
            //
            // // Уничтожаем объект эффекта после duration
            // GameObject.Destroy(effectObject, spell.duration);
        }

        private void CastTargetAoe(TargetAoeSpellData targetAoeSpell, Vector3 worldPosition) =>
            CastAoe(targetAoeSpell, worldPosition);
        
        private void CastAoe(AoeSpellData aoeSpell, Vector3 worldPosition)
        {
            var aoe = aoeSpell.visualEffect
                ? Object.Instantiate(aoeSpell.visualEffect, m_casterTransform.position, Quaternion.identity)
                : new GameObject();
            
            aoe.transform.position = worldPosition;
            
            var spellAoe = 
                aoe.GetComponent<ISpellAoe>() ??
                aoe.AddComponent<SpellAoe>();
            
            spellAoe.Initialize(worldPosition, aoeSpell.radius, aoeSpell.effects);
        }
    }
}

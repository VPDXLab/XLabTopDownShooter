using System;
using UnityEngine;
using Magic.Effects;
using Magic.Buffs.Extensions;
using System.Collections.Generic;

namespace Magic.Buffs
{
    public sealed class BuffContainer : MonoBehaviour, IEffectable
    {
        public event Action<IBuff> BuffAdded;
        public event Action<IBuff> BuffRemoved;
        
        private HashSet<string> m_ids = new();
        private Dictionary<string, IBuff>  m_buffs = new();
        
        public IReadOnlyCollection<IBuff> Buffs => m_buffs.Values;

        public void Add(IBuff buff)
        {
            if (m_buffs.TryGetValue(buff.Id, out IBuff existingBuff))
            {
                existingBuff.Refresh(this);
                m_ids.Remove(existingBuff.Id);
            }
            else
            {
                m_buffs.Add(buff.Id, buff);
                buff.Initialize(this);
                                
                BuffAdded?.Invoke(buff);
            }
        }

        public void Remove(IBuff buff)
        {
            m_ids.Add(buff.Id);
        }

        public void Update()
        {
            foreach (var buff in m_buffs.Values)
            {
                buff.Update(Time.deltaTime);
            }

            foreach (var id in m_ids)
            {
                var buff = m_buffs[id];
                
                m_buffs.Remove(id);
                BuffRemoved?.Invoke(buff);
            }
            
            m_ids.Clear();
        }
    }
}
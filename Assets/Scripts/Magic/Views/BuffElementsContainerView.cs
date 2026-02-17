using System.Collections.Generic;
using Magic.Buffs;
using UnityEngine;

namespace Magic.Views
{
    public sealed class BuffElementsContainerView : MonoBehaviour
    {
        [SerializeField] private BuffElementView m_buffView;
        [SerializeField] private BuffElementView m_debuffView;
        [SerializeField] private BuffContainer m_buffContainer;

        private Dictionary<string, BuffElementView> m_elements = new();
        
        private void OnEnable()
        {
            foreach (var buff in m_buffContainer.Buffs)
            {
                AddElement(buff);
            }
            
            m_buffContainer.BuffAdded += AddElement;
            m_buffContainer.BuffRemoved += RemoveElement;
        }

        private void OnDisable()
        {
            foreach (var buff in m_buffContainer.Buffs)
            {
                RemoveElement(buff);
            }
            
            m_buffContainer.BuffAdded -= AddElement;
            m_buffContainer.BuffRemoved -= RemoveElement;
        }

        private void AddElement(IBuff buff)
        {
            var element = buff.Type is BuffType.Buff
                ? Instantiate(m_buffView, transform)
                : Instantiate(m_debuffView, transform);
            
            element.Initialize(buff);
            m_elements.Add(buff.Id, element);
        }

        private void RemoveElement(IBuff buff)
        {
            var element = m_elements[buff.Id];
            element.Deinitialize();
            
            Destroy(element);
            m_elements.Remove(buff.Id);
        }
    }
}
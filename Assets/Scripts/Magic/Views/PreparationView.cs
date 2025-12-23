using Magic.Data;
using UnityEngine;
using DG.Tweening;
using System.Linq;
using Magic.Systems;
using UnityEngine.UI;
using Magic.Elements;
using System.Collections.Generic;

namespace Magic.Views
{
    public sealed class PreparationSpellView : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private MagicSystem m_magicSystem;
        [SerializeField] private MagicConfig m_config;
        
        [SerializeField] private RectTransform m_elementsContainer;
        
        [SerializeField] private Image[] m_icons;
        
        [Header("Animation")]
        [SerializeField] private float m_shakeIntensity = 20f;
        [SerializeField] private float m_shakeDuration = 1f;
        
        private int m_activeCount;
        private Tween m_shakeTween;
        
        private void OnEnable()
        {
            m_magicSystem.ElementsChanged += UpdateIcons;
            m_magicSystem.SpellCancelled += ShakeContainer;
        }
        
        private void OnDisable()
        {
            m_magicSystem.ElementsChanged -= UpdateIcons;
            m_magicSystem.SpellCancelled -= ShakeContainer;
        }
        
        private void UpdateIcons(IReadOnlyList<ElementType> elements)
        {
            foreach (var icon in m_icons)
            {
                icon.sprite = null;
                icon.enabled = false;
            }
            
            if (elements is null || elements.Count is 0)
            {
                return;
            }
            
            for (var i = 0; i < elements.Count; i++)
            {
                var elementInfo = GetElementInfo(elements[i]);
                
                m_icons[i].enabled = true;
                m_icons[i].sprite = elementInfo.icon;
            }
        }
     
        private void ShakeContainer()
        {
            m_shakeTween?.Kill();
            
            var localRotation = m_elementsContainer.localRotation;
            
            m_shakeTween = m_elementsContainer
                .DOShakeRotation(m_shakeDuration, m_shakeIntensity)
                .SetEase(Ease.OutQuad)
                .OnComplete(() => m_elementsContainer.localRotation = localRotation);
        }
        
        private ElementsData.Item GetElementInfo(ElementType type) =>
            m_config.ElementsData.Items.FirstOrDefault(item => item.type == type);
    }
}
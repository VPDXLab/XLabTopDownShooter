using UnityEngine;

namespace Utils
{
    public sealed class FixedRotation : MonoBehaviour
    {
        private Transform m_parent;
        private Vector3 m_worldOffset;
        private Quaternion m_fixedRotation;
        
        private void Start()
        {
            m_parent = transform.parent;
            
            m_fixedRotation = transform.rotation;
            m_worldOffset = transform.position - m_parent.position;
        }
        
        private void LateUpdate()
        {
            if (!m_parent)
            {
                return;
            }
            
            transform.position = m_parent.position + m_worldOffset;
            transform.rotation = m_fixedRotation;
        }
        
    }
}

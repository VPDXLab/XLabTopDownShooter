using UnityEngine;

namespace Utils
{
    public class FixedRotation : MonoBehaviour
    {
        private Quaternion m_fixedRotation;
        
        private void Start() =>
            m_fixedRotation = transform.rotation;
        
        private void LateUpdate() =>
            transform.rotation = m_fixedRotation;
    }
}

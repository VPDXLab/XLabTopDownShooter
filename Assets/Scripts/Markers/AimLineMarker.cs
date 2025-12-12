using UnityEngine;
using UnityEngine.InputSystem;

namespace Markers
{
    [RequireComponent(typeof(LineRenderer))]
    public class AimLineMarker : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Transform m_playerTransform;
        [SerializeField] private LineRenderer m_lineRenderer;

        [Header("Settings")]
        [SerializeField] [Min(0)] private float m_zOffset = 0.5f;
        [SerializeField] [Min(0)] private float m_lineWidth = 0.1f;
        [SerializeField] [Min(0)] private float m_disableDistance = 1f;

        private Camera m_camera;

        private void OnValidate()
        {
            if (!m_lineRenderer)
            {
                m_lineRenderer = GetComponent<LineRenderer>();
            }
        }

        private void Awake()
        {
            m_camera = Camera.main;
            m_lineRenderer.positionCount = 2;
            m_lineRenderer.startWidth = m_lineWidth;
            m_lineRenderer.endWidth = m_lineWidth;
        }

        private void LateUpdate()
        {
            var playerPos = m_playerTransform.position;
            var end = GetAimPosition();
            
            var direction = (end - playerPos).normalized;
            var start = playerPos + direction * m_zOffset;
            
            start.y = playerPos.y;
            end.y = playerPos.y;

            m_lineRenderer.SetPosition(index: 0, start);
            m_lineRenderer.SetPosition(index: 1, end);
            m_lineRenderer.enabled = Vector3.Distance(start, end) > m_disableDistance;
        }

        private Vector3 GetAimPosition()
        {
            var mousePosition = Mouse.current.position.ReadValue();
            
            var ray = m_camera.ScreenPointToRay(mousePosition);
            var groundPlane = new Plane(inNormal: Vector3.up, inPoint: new Vector3(0, m_playerTransform.position.y, 0));
            
            if (groundPlane.Raycast(ray, out var distance))
            {
                return ray.GetPoint(distance);
            }
            
            return m_playerTransform.position + m_playerTransform.forward;
        }
    }
}

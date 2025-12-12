using Inputs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Players
{
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerConfig m_config;
        [SerializeField] private MouseResolver m_mouseResolver;
        [SerializeField] private PlayerMovement m_playerMovement;
        
        private PlayerRotationCalculator m_playerRotationCalculator;

        private void OnValidate()
        {
            if (!m_playerMovement)
            {
                m_playerMovement = GetComponent<PlayerMovement>();
            }
        }

        private void Start()
        {
            var camera = Camera.main;
            
            m_playerMovement.Initialize(m_config.speed, m_config.angularSpeed);
            m_playerRotationCalculator = new PlayerRotationCalculator(transform, camera);
            
            SetupCursor();
        }

        private void Update()
        {
            Vector3 mousePosition = Mouse.current.position.ReadValue();
            
            var lookPoint = m_playerRotationCalculator.Calculate(mousePosition);
            m_playerMovement.RotateTowards(lookPoint);
            
            if (Mouse.current.rightButton.wasPressedThisFrame)
            {
                var navPoint = m_mouseResolver.GetNavMeshPoint();
                
                if (navPoint.HasValue)
                {
                    m_playerMovement.SetDestination(navPoint.Value);
                }
            }
        }

        private void SetupCursor()
        {
            Texture2D cursorTexture = m_config.cursorTexture;

            if (cursorTexture is not null)
            {
                var hotspot = new Vector2(cursorTexture.width / 2f, cursorTexture.height / 2f);
                Cursor.SetCursor(m_config.cursorTexture, hotspot, CursorMode.Auto);
            }
        }
    }
}

using UnityEngine;

namespace Player
{
    public class PlayerInteract : MonoBehaviour
    {
        private Camera _camera;

        [SerializeField] private float _distance = 3f;
        [SerializeField] private LayerMask _mask;

        private PlayerUI _playerUI;
        private InputManager _inputManager;
        private void Start()
        {
            _camera = GetComponent<PlayerLook>()._camera;
            _playerUI = GetComponent<PlayerUI>();
            _inputManager = GetComponent<InputManager>();
        }

        private void Update()
        {
            _playerUI.UpdateText(string.Empty);
            
            Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);
            Debug.DrawRay(ray.origin,ray.direction * _distance);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, _distance, _mask))
            {
                if (hitInfo.collider.GetComponent<Interactable>() != null)
                {
                    Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                    _playerUI.UpdateText(interactable.promptMessage);
                    if (_inputManager.onFoot.Interact.triggered)
                    {
                        interactable.BaseInteract();
                    }
                }   
            }
        }
    }
}

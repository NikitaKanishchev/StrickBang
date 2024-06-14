using UnityEngine;

namespace Player
{
    public class PlayerMotor : MonoBehaviour
    {
        private CharacterController _controller;
        private Vector3 playerVelocity;
        private bool isGrounded;
        public float speed = 5f;
        public float gravity = -9.8f;
        public float jumpHeight = 1.5f;

        private bool crouching;
        private float crouchTimer;
        private bool lerpCrounch;

        private bool sprinting;


        private void Start()
        {
            _controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            isGrounded = _controller.isGrounded;
            if (lerpCrounch)
            {
                crouchTimer += Time.deltaTime;
                float p = crouchTimer / 1;
                p *= p;
                if (crouching)
                    _controller.height = Mathf.Lerp(_controller.height, 1, p);
                else
                    _controller.height = Mathf.Lerp(_controller.height, 2, p);

                if (p > 1)
                    lerpCrounch = false;
                crouchTimer = 0f;
            }
        }

        public void ProcessMove(Vector2 input)
        {
            Vector3 moveDirection = Vector3.zero;
            moveDirection.x = input.x;
            moveDirection.z = input.y;

            _controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
            playerVelocity.y += gravity * Time.deltaTime;
            if (isGrounded && playerVelocity.y < 0)
                playerVelocity.y = -2f;
            _controller.Move(playerVelocity * Time.deltaTime);
        }

        public void Jump()
        {
            if (isGrounded)
            {
                playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
            }
        }

        public void Crouch()
        {
            crouching = !crouching;
            crouchTimer = 0;
            lerpCrounch = true;
        }

        public void Sprint()
        {
            sprinting = !sprinting;
            if(sprinting)
                speed = 9;
            else
                speed = 5;
        }
    }
}
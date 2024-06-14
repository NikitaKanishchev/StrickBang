using UnityEngine;

namespace Player
{
    public class PlayerLook : MonoBehaviour
    {
        public Camera _camera;

        private float xRotation = 0f;
        public float xSensavity = 30f;
        public float ySensavity = 30f;

        public void ProcessLook(Vector2 input)
        {
            float mouseX = input.x;
            float mouseY = input.y;
            //rotate look up and down
            xRotation -= (mouseY * Time.deltaTime) * ySensavity;
            xRotation = Mathf.Clamp(xRotation, -80f, 80f);
            //camera transform
            _camera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            //rotate player left and right
            transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensavity);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using WinGL;

namespace RotatinCubeScene
{
    public class Camera
    {
        private float _fov;
        private float _near;
        private float _far;

        private Matrix4x4 _viewMatrix;
        private Matrix4x4 _projectionMatrix;

        private Vector3 _position = new Vector3(0, 0, 7);
        private Vector3 _lookDirection = new Vector3(0, -0.3f, -1.0f);
        private const float _moveSpeed = 10.0f;

        private float _yaw;
        private float _pitch;

        private Vector2 _mousePressedPos;
        private bool _mousePressed = false;
        private float _windowWidth, _windowHeight;


        public Matrix4x4 ViewMatrix { get => _viewMatrix; set { _viewMatrix = value; } }
        public Matrix4x4 ProjectionMatrix { get => _projectionMatrix; set { _projectionMatrix = value; } }

        public Vector3 Position
        {
            get => _position;
            set
            {
                _position = value;
                UpdateViewMatrix();
            }
        }
        public Vector3 LookDirection => _lookDirection;

        public float FOV => _fov;
        public float AspectRatio => _windowWidth / _windowHeight;

        public float Yaw
        {
            get => _yaw;
            set
            {
                _yaw = value;
                UpdateViewMatrix();
            }
        }
        public float Pitch
        {
            get => _pitch;
            set
            {
                _pitch = Math.Clamp(value, -MathF.PI / 2 + 0.01f, MathF.PI / 2 - 0.01f);
                UpdateViewMatrix();
            }
        }
        public Camera(float windowWidth, float windowHeight, float fov = 45.0f, float nearPlane = 0.1f, float farPlane = 1000.0f)
        {
            _windowWidth = windowWidth;
            _windowHeight = windowHeight;
            _fov = MathF.PI * fov / 180.0f; // radians
            _near = nearPlane;
            _far = farPlane;

            _yaw = -MathF.PI / 2; // -90 degrees
            _pitch = 0.0f;

            _projectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView(_fov, AspectRatio, _near, _far);
            UpdateViewMatrix();
        }
        public void Update(float dt)
        {
            HandleMouseInput();
            HandleKeyboardInput(dt);
            UpdateProjectionMatrix();
        }
        private void HandleMouseInput()
        {
            if (Input.Instance.IsMousePress(MouseCode.MouseRight))
            {
                Vector2 currentMousePos = new Vector2((float)Input.Instance.MouseX, (float)Input.Instance.MouseY);
                if (_mousePressed)
                {
                    Vector2 delta = currentMousePos - _mousePressedPos;
                    float sensitivity = 0.002f;
                    _yaw += delta.X * sensitivity;
                    _pitch -= delta.Y * sensitivity;
                }
                _mousePressed = true;
                _mousePressedPos = currentMousePos;
                UpdateViewMatrix();
            }
            else
            {
                _mousePressed = false;
            }
        }
        private void UpdateViewMatrix()
        {
            _lookDirection.X = MathF.Cos(_pitch) * MathF.Cos(_yaw);
            _lookDirection.Y = MathF.Sin(_pitch);
            _lookDirection.Z = MathF.Cos(_pitch) * MathF.Sin(_yaw);
            _lookDirection = Vector3.Normalize(_lookDirection);

            Vector3 target = _position + _lookDirection;

            _viewMatrix = Matrix4x4.CreateLookAt(_position, target, Vector3.UnitY);
        }
        public void ProcessMouseMovement(float deltaX, float deltaY, float sensitivity = 0.002f)
        {
            _yaw += deltaX * sensitivity;
            _pitch -= deltaY * sensitivity;

            _pitch = Math.Clamp(_pitch, -MathF.PI / 2 + 0.01f, MathF.PI / 2 - 0.01f);

            UpdateViewMatrix();
        }
        private void HandleKeyboardInput(float deltaTime)
        {
            if (Input.Instance.IsKeyDown(KeyCode.W)) { _position += LookDirection * _moveSpeed * deltaTime; }
            if (Input.Instance.IsKeyDown(KeyCode.S)) { _position -= LookDirection * _moveSpeed * deltaTime; }
            if (Input.Instance.IsKeyDown(KeyCode.A)) { _position -= GetRight() * _moveSpeed * deltaTime; }
            if (Input.Instance.IsKeyDown(KeyCode.D)) { _position += GetRight() * _moveSpeed * deltaTime; }
            if (Input.Instance.IsKeyDown(KeyCode.Space)) { _position += GetUp() * _moveSpeed * deltaTime; }
            if (Input.Instance.IsKeyDown(KeyCode.Shift)) { _position -= GetUp() * _moveSpeed * deltaTime; }

            UpdateViewMatrix();
        }
        private Vector3 GetRight()
        {
            return Vector3.Normalize(Vector3.Cross(_lookDirection, Vector3.UnitY));
        }
        private Vector3 GetUp()
        {
            return Vector3.Normalize(Vector3.Cross(GetRight(), _lookDirection));
        }
        private void UpdateProjectionMatrix()
        {
            _projectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView(_fov, AspectRatio, _near, _far);
        }
    }
}

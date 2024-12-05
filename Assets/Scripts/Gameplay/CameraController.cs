using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private Transform _player;
    [Header("Free Look")]
    [SerializeField] private float _mouseSensitivity = 2.0f;
    [SerializeField] private float _rotationSmoothTime = 0.12f;
    [SerializeField] private Vector2 _verticalClamp = new Vector2(-10, 80);
    [SerializeField] private float _cameraDistance = 5.0f;
    [SerializeField] private Vector2 _freeLookOffset;
    [Header("Follow")]
    [SerializeField] private float _followSpeed = 5.0f;
    [SerializeField] private Vector2 _followOffset;


    private float _rotationX = 0.0f;
    private float _rotationY = 0.0f;
    private Vector3 _currentVelocity;

    private float _targetRotationX = 0.0f;
    private float _targetRotationY = 0.0f;

    private bool _isFreeLook = true;

    private static CameraController _singleton;

    private void Awake()
    {
        _singleton = this;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (_isFreeLook)
        {
            float _mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity;
            float _mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity;

            _targetRotationX -= _mouseY;
            _targetRotationY += _mouseX;

            _targetRotationX = Mathf.Clamp(_targetRotationX, -_verticalClamp.x, _verticalClamp.y);

            _rotationX = Mathf.SmoothDamp(_rotationX, _targetRotationX, ref _currentVelocity.x, _rotationSmoothTime);
            _rotationY = Mathf.SmoothDamp(_rotationY, _targetRotationY, ref _currentVelocity.y, _rotationSmoothTime);

            transform.rotation = Quaternion.Euler(_rotationX, _rotationY, 0);
            transform.position = _player.position + transform.forward * _freeLookOffset.x + Vector3.up * _freeLookOffset.y;
        }
        else
        {
            Vector3 desiredPosition = _player.position + transform.forward * _followOffset.x + Vector3.up * _followOffset.y;

            // Smoothly move the camera to the desired position using SmoothDamp
            transform.position = Vector3.SmoothDamp(transform.position, 
                desiredPosition, ref _currentVelocity, _followSpeed * Time.deltaTime);
        }
        
    }

    public static CameraController Get()
    {
        return _singleton;
    }

    public void ToggleCameraMode(bool isFreeLook)
    {
        _isFreeLook = isFreeLook;
    }
}

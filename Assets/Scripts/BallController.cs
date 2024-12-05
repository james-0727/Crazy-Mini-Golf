using UnityEngine;

/// <summary>
/// Basic Ball Controller
/// </summary>

public class BallController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigid;
    [SerializeField] private float _powerMultiplier = 10f;
    [SerializeField] private float _maxPower = 20f;
    [SerializeField] private float _aimSensitivity = 1f;
    [SerializeField] private float _rotationSpeed = 2f;
    [SerializeField] private LineRenderer _lineRenderer;

    private Vector3 _shotDirection;
    private float _shotPower = 0f;

    private bool _isAiming = false;
    private bool _isShot = false;

    private float _aimStartTime;

    void Start()
    {
        if (_rigid == null)
        {
            _rigid = GetComponent<Rigidbody>();
        }

        _rigid.maxAngularVelocity = 100f;
        _rigid.drag = 1f;
    }

    void Update()
    {
        if (!_isShot)
        {
            HandleAiming();
            HandleShooting();
        }

        if (_rigid.velocity.magnitude > 0.1f)
        {
            _rigid.angularVelocity = _rigid.velocity * _rotationSpeed;
        }
        else
        {
            ResetBall(transform.position);
        }
    }

    private void HandleAiming()
    {
        if (!_isAiming)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _isAiming = true;
                _aimStartTime = Time.time;
                CameraController.Get().ToggleCameraMode(true);
                _lineRenderer.enabled = true;
            }
        }
        else
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10f;
            Vector3 direction = Camera.main.transform.forward;
            direction.y = 0f;

            _shotDirection = direction.normalized;
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, transform.position + _shotDirection * 5);
        }
    }

    private void HandleShooting()
    {
        if (_isAiming && Input.GetMouseButton(0))
        {
            _shotPower = Mathf.Clamp((Time.time - _aimStartTime) * _aimSensitivity, 0, _maxPower);
        }

        if (_isAiming && Input.GetMouseButtonUp(0))
        {
            _rigid.AddForce(_shotDirection * _shotPower * _powerMultiplier, ForceMode.Impulse);
            CameraController.Get().ToggleCameraMode(false);

            // Reset aiming state
            _isAiming = false;
            _isShot = true;
            _lineRenderer.enabled = false;
        }
    }

    // Reset the ball after a shot
    public void ResetBall(Vector3 startPosition)
    {
        transform.position = startPosition;
        _rigid.velocity = Vector3.zero;
        _rigid.angularVelocity = Vector3.zero;
        _isShot = false;
    }
}

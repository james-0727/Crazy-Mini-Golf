using System.Transactions;
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
    [SerializeField] private float _stoppingMagnitude = 0.1f;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private float _minimumHeight = -5;

    private Vector3 _shotDirection;
    private float _shotPower = 0f;

    private bool _isAiming = false;
    private bool _isShot = false;

    private float _aimStartTime;
    private float _lastMagnitude;
    private Vector3 _lastAimPosition;

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
        else
        {
            if (_rigid.velocity.magnitude > _stoppingMagnitude)
            {
                _rigid.angularVelocity = _rigid.velocity * _rotationSpeed;
            }
            else if (_rigid.velocity.magnitude < _lastMagnitude)
            {
                ResetBall(transform.position);
                CameraController.Get().ToggleCameraMode(true);
            }

            // check if ball is out of track
            if (transform.position.y < _minimumHeight)
            {
                ResetBall(_lastAimPosition);
            }

            _lastMagnitude = _rigid.velocity.magnitude;
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
                _lineRenderer.enabled = true;
                _lastAimPosition = transform.position;
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
            GameUI.Get().PowerSlider.value = _shotPower / _maxPower;
        }

        if (_isAiming && Input.GetMouseButtonUp(0))
        {
            _rigid.AddForce(_shotDirection * _shotPower * _powerMultiplier, ForceMode.Impulse);
            CameraController.Get().ToggleCameraMode(false);
            GameUI.Get().PowerSlider.value = 0;

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
        _lastMagnitude = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Winning Hole")
        {
            GameManager.Get().EndGame();
        }
    }
}

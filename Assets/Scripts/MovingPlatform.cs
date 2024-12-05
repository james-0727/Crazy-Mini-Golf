using UnityEngine;
/// <summary>
/// Moving platform
/// </summary>

public class MovingPlatform : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private Vector3 _moveAxis = Vector3.right;
    [SerializeField] private float _moveDistance = 5f;
    [SerializeField] private float _speed = 2f;

    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    private bool _movingForward = true;

    private void Start()
    {
        _startPosition = transform.position;
        _targetPosition = _startPosition + _moveAxis.normalized * _moveDistance;
    }

    private void Update()
    {
        // Move the platform back and forth
        if (_movingForward)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, _targetPosition) < 0.01f)
            {
                _movingForward = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _startPosition, _speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, _startPosition) < 0.01f)
            {
                _movingForward = true;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize the movement path in the editor
        if (Application.isPlaying)
        {
            return;
        }

        Gizmos.color = Color.red;
        Vector3 targetPosition = transform.position + _moveAxis.normalized * _moveDistance;
        Gizmos.DrawLine(transform.position, targetPosition);
        Gizmos.DrawSphere(targetPosition, 0.1f);
    }

}

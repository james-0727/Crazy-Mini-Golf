using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private Vector3 _launchDirection = Vector3.up;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Rigidbody rb))
        {
            rb.AddForce(_launchDirection.normalized * _jumpForce, ForceMode.VelocityChange);
        }
    }
}

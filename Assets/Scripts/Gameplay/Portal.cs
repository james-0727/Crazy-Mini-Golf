using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour
{
    [SerializeField] private Transform _linkedPortal;
    [SerializeField] private Transform _exitPoint;
    [SerializeField] private float _teleportDelay = 0.1f;

    private bool _isTeleporting = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!_isTeleporting && _linkedPortal != null)
        {
            StartCoroutine(Teleport(other));
        }
    }

    private IEnumerator Teleport(Collider objectToTeleport)
    {
        _isTeleporting = true;
        Vector3 offset = objectToTeleport.transform.position - transform.position;
        objectToTeleport.transform.position = (_exitPoint != null ? _exitPoint.position : _linkedPortal.position) + offset;
        objectToTeleport.transform.rotation = _linkedPortal.rotation;

        yield return new WaitForSeconds(_teleportDelay);
        _isTeleporting = false;
    }
}

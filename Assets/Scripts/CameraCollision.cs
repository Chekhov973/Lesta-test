using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private float _minDistance;
    [SerializeField] private float _maxDistance;
    [SerializeField] private float _offset;
    private float _currentDistance;


    private void CalculateCameraCollision()
    {
        var heading = _cameraTransform.position - _targetTransform.position;
        var distance = heading.magnitude;
        var direction = heading / distance;

        if (Physics.Raycast(_targetTransform.position, direction, out RaycastHit hit, _maxDistance + _offset))
        {
            float dist = Vector3.Distance(_targetTransform.position, hit.point);

            _currentDistance = Mathf.Clamp(dist, _minDistance, _maxDistance);
        }
        else
        {
            _currentDistance = _maxDistance;
        }

        _cameraTransform.localPosition =  new Vector3(0, 0, -_currentDistance);
    }

    private void LateUpdate()
    {
        CalculateCameraCollision();
    }

    //private void OnDrawGizmos()
    //{
    //    var heading = _cameraTransform.position - _targetTransform.position;
    //    var distance = heading.magnitude;
    //    var direction = heading / distance;
    //
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawRay(_targetTransform.position, direction);
    //}

    private void LerpLocalPosition(float distancePercentage)
    {
        
    }
}

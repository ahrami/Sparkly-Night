using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField] private Transform _target;
    [SerializeField] private bool _followX = true;
    [SerializeField] private bool _followY = true;
    [SerializeField] private float _offsetY = 0f;
    private Vector3 _targetPosition;

    private void FixedUpdate() {
        if (_followX) {
            _targetPosition.x = _target.position.x;
        } else {
            _targetPosition.x = transform.position.x;
        }
        if (_followY) {
            _targetPosition.y = _target.position.y + _offsetY;
        } else {
            _targetPosition.y = transform.position.y;
        }
        _targetPosition.z = transform.position.z;
        transform.Translate(_targetPosition - transform.position);
    }
}

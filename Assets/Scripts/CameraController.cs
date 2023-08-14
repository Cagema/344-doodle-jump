using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float _dumping = 1.5f;
    [SerializeField] Vector2 _offset = new(2, 1);

    Transform _playerTr;
    float _maxHeight = 5;

    private void Start()
    {
        _offset = new Vector2(Mathf.Abs(_offset.x), _offset.y);
        _playerTr =FindObjectOfType<Player>().transform;
    }

    private void FixedUpdate()
    {
        if (_playerTr != null)
        {
            if (_playerTr.position.y > _maxHeight) _maxHeight = _playerTr.position.y;
            Vector3 target = new( _offset.x, _maxHeight, transform.position.z);

            transform.position = Vector3.Lerp(transform.position, target, _dumping * Time.deltaTime);
        }
    }
}

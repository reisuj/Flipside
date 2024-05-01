using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerRotation : MonoBehaviour
{    
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _pivotPoint;
    [SerializeField] private float _targetRotation = 180.0f;
    [SerializeField] private float _darkWorldRotation = 180.0f;
    [SerializeField] private float _lightWorldRotation = 180.0f;
    [SerializeField] private float _currentRotation = 0.0f;
    [SerializeField] private bool _isRotating = false;
    [SerializeField] private bool _isLightWorld = false;
    // Start is called before the first frame update
    void Start()
    {
        _isLightWorld = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            _pivotPoint.transform.position = new Vector3(_player.transform.position.x, 0, 0);

            if (_isLightWorld)
            {
                _player.GetComponent<PlayerMovement>().DisablePlayerPhysics(0.0f);
                StartCoroutine(DarkWorldRotate());
            }
            else
            {
                _player.GetComponent<PlayerMovement>().DisablePlayerPhysics(0.0f);
                StartCoroutine(LightWorldRotate());
            }
        }
    }

    private IEnumerator DarkWorldRotate()
    {
        _isRotating = true;
        while (_isRotating)
        {
            _player.transform.RotateAround(_pivotPoint.transform.position, Vector3.forward, _targetRotation / 10.0f);
            _camera.transform.RotateAround(_pivotPoint.transform.position, Vector3.forward, _targetRotation / 10.0f);
            _currentRotation += _targetRotation / 10.0f;
            yield return new WaitForSeconds(0.1f);
            if (_currentRotation == _darkWorldRotation)
            {
                _isRotating = false;
            }
        }
        _player.GetComponent<PlayerMovement>().EnablePlayerPhysics(-1.0f, -5.0f, -1.0f);
        _isLightWorld = false;
        _player.GetComponent<PlayerMovement>().DarkWorldActive();
        yield return null;
    }

    private IEnumerator LightWorldRotate()
    {
        _isRotating = true;
        while (_isRotating)
        {
            _player.transform.RotateAround(_pivotPoint.transform.position, Vector3.back, _targetRotation / 10.0f);
            _camera.transform.RotateAround(_pivotPoint.transform.position, Vector3.back, _targetRotation / 10.0f);
            _currentRotation -= _targetRotation / 10.0f;
            yield return new WaitForSeconds(0.1f);
            if (_currentRotation == _lightWorldRotation)
            {
                _isRotating = false;
            }
        }
        _player.GetComponent<PlayerMovement>().EnablePlayerPhysics(1.0f, 5.0f, 1.0f);
        _isLightWorld = true;
        _player.GetComponent<PlayerMovement>().LightWorldActive();
        yield return null;
    }
}

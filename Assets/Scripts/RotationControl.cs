using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationControl : MonoBehaviour
{
    [SerializeField] private GameObject _pivotPoint; // Reference to the pivot point GameObject
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _world;
    [SerializeField] private float _targetRotation = 180f;
    [SerializeField] private float _currentRotation = 0.0f;
    [SerializeField] private bool _flipping;
    [SerializeField] private bool _darkWorld;
    [SerializeField] private float _darkWorldRotation = 180f;
    [SerializeField] private bool _lightWorld;
    [SerializeField] private float _lightWorldRotation = 0.0f;
    [SerializeField] private float _currentWorld;
    private void Start()
    {
        _lightWorld = true;
    }

    void Update()
    {
        _pivotPoint.transform.position = new Vector3(_player.transform.position.x, 0, 0);
        // Check for the "R" key press
        if (Input.GetKeyDown(KeyCode.R))
        {
            
            if (_lightWorld)
            {
                _player.GetComponent<PlayerMovement>().DisablePlayerPhysics();
                StartCoroutine(DarkWorldRotate());
            }
            
            if (_darkWorld)
            {
                _player.GetComponent<PlayerMovement>().DisablePlayerPhysics();
                StartCoroutine(LightWorldRotate());
            }
        }
    }
    private IEnumerator DarkWorldRotate()
    {
        _flipping = true;
        while(_flipping)
        {
            _world.transform.RotateAround(_pivotPoint.transform.position, (Vector3.forward), _targetRotation / 10.0f);
            _currentRotation += _targetRotation / 10.0f;            
            yield return new WaitForSeconds(0.1f);
            if (_currentRotation == _darkWorldRotation)
            {
                _flipping = false;
            }
        }
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        _player.GetComponent<PlayerMovement>().EnablePlayerPhysics();
        _lightWorld = false;
        _darkWorld = true;
        yield return null;
    }

    private IEnumerator LightWorldRotate()
    {
        _flipping = true;
        while (_flipping)
        {
            _world.transform.RotateAround(_pivotPoint.transform.position, (Vector3.back), _targetRotation / 10.0f);
            _currentRotation -= _targetRotation / 10.0f;
            yield return new WaitForSeconds(0.1f);
            if (_currentRotation == _lightWorldRotation)
            {
                _flipping = false;
            }
        }
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        _player.GetComponent<PlayerMovement>().EnablePlayerPhysics();
        _darkWorld = false;
        _lightWorld = true;
        yield return null;
    }
}


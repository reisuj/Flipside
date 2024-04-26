using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerRotation : MonoBehaviour
{
    //[SerializeField] private PlayerMovement _player;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _pivotPoint;
    [SerializeField] private float _darkWorldRotation = 180.0f;
    [SerializeField] private float _lightWorldRotation = 0.0f;
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
        _pivotPoint.transform.position = new Vector3(_player.transform.position.x, 0, 0);

        if (Input.GetKeyDown(KeyCode.R))
        {
            _player.GetComponent<PlayerMovement>().DisablePlayerPhysics();
            if (_isLightWorld)
            {                
                StartCoroutine(DarkWorldRotate());
            }
            else
            {
                StartCoroutine(LightWorldRotate());
            }
        }
    }

    private IEnumerator DarkWorldRotate()
    {
        _isRotating = true;
        while (_isRotating)
        {
            _player.transform.RotateAround(_pivotPoint.transform.position, Vector3.forward, _darkWorldRotation / 10.0f);
            _currentRotation += _darkWorldRotation / 10.0f;
            yield return new WaitForSeconds(0.1f);
            if (_currentRotation == _darkWorldRotation)
            {
                _isRotating = false;
            }
        }        
        _player.GetComponent<PlayerMovement>().EnablePlayerPhysics();
        _isLightWorld = false;
        yield return null;
    }

    private IEnumerator LightWorldRotate()
    {
        _isRotating = true;
        yield return null;
    }
}

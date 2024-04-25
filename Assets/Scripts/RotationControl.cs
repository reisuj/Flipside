using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationControl : MonoBehaviour
{
    [SerializeField] private GameObject _pivotPoint; // Reference to the pivot point GameObject
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _world;

    private void Start()
    {
        
    }

    void Update()
    {
        _pivotPoint.transform.position = new Vector3(_player.transform.position.x, 0, 0);
        // Check for the "R" key press
        if (Input.GetKeyDown(KeyCode.R))
        {
            RotateObjects();
        }
    }

    void RotateObjects()
    {
        // Rotate the pivot point by 180 degrees around the Z-axis
        //_pivotPoint.transform.Rotate(Vector3.forward, 180f);
        _world.transform.RotateAround(_pivotPoint.transform.position, Vector3.forward, 180f);
    }
}


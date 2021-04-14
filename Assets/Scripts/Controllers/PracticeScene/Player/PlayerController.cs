using System;
using System.Collections;
using System.Collections.Generic;
using Diploma.Interfaces;
using UnityEngine;

public class PlayerController
{
    private readonly GameObject _playerGameObject;
    
    private readonly float _mouseSensitivity;
    private readonly Camera _camera;
    private readonly float _speed;

    private readonly Transform _pickUpParent;
    
    private readonly float _gravity = 0f;

    private readonly float _mouseSmoothTime = 0.02f;
    private Vector2 _currentMouseDelta = Vector2.zero;
    private Vector2 _currentMouseDeltaVelocity = Vector2.zero;
    
    private readonly CharacterController _playerController;
    private float _yAxisRotation;

    private float _gravityVelocity;
    
    private bool _isHandsBusy;

    private GameObject _objectHitted;
    public static Action<GameObject> OnPartClicked;
    
    public PlayerController(GameObject playerGameObject)
    {
        _speed = 2f;
        _mouseSensitivity = 2f;
        
        _playerGameObject = playerGameObject;

        _playerController = _playerGameObject.GetComponent<CharacterController>();
        _camera = Camera.main;
        _pickUpParent = _playerGameObject.GetComponentsInChildren<Transform>()[2];
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void RotateCamera()
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"),Input.GetAxis("Mouse Y"));
        _currentMouseDelta = Vector2.SmoothDamp(_currentMouseDelta, targetMouseDelta,
            ref _currentMouseDeltaVelocity, _mouseSmoothTime);

        _yAxisRotation -= _currentMouseDelta.y * _mouseSensitivity;
        _yAxisRotation = Mathf.Clamp(_yAxisRotation, -90f, 90f);
        
        _camera.transform.localEulerAngles = Vector3.right * _yAxisRotation;
        _playerGameObject.transform.Rotate(Vector3.up * (_currentMouseDelta.x * _mouseSensitivity));
    }

    public void MovePlayer()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        if (_playerController.isGrounded)
            _gravityVelocity = 0f;

        _gravityVelocity += _gravity * Time.deltaTime;

        Vector3 velocity = (_playerGameObject.transform.forward * movement.z + 
                            _playerGameObject.transform.right * movement.x) * _speed + Vector3.up * _gravityVelocity;

        _playerController.Move(velocity * Time.deltaTime);
    }

    public void PickUp(GameObject objectToPickUp)
    {
        if (!_isHandsBusy)
        {
            GameObject.Instantiate(objectToPickUp, _pickUpParent.position, _pickUpParent.rotation, _pickUpParent);
            _isHandsBusy = true;
        }
        else
        {
            GameObject.Destroy(_pickUpParent.transform.GetChild(0).gameObject);
            GameObject.Instantiate(objectToPickUp, _pickUpParent.position, _pickUpParent.rotation, _pickUpParent);
        }
    }

    public void OutlineAssemblyParts()
    {
        Ray hitRay = _camera.ScreenPointToRay(new Vector3(Screen.currentResolution.width / 2f, Screen.currentResolution.height / 2f, 0));
        if (Physics.Raycast(hitRay, out var hit, 10))
        {
            if (hit.collider.CompareTag("Assembly"))
            {
                Debug.DrawLine(_camera.transform.position, hit.point);
                _objectHitted = hit.collider.gameObject;
                if(Input.GetMouseButtonDown(0))
                    OnPartClicked?.Invoke(_objectHitted);
            }
        }
    }
}

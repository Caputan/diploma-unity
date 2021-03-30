using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private Transform camera;
    [SerializeField] private float speed;

    [SerializeField] private Transform pickUpParent;
    
    [SerializeField] private float gravity = -12f;

    [SerializeField] [Range(0f, 0.5f)] private float mouseSmoothTime = 0.02f;
    private Vector2 _currentMouseDelta = Vector2.zero;
    private Vector2 _currentMouseDeltaVelocity = Vector2.zero;
    
    private CharacterController _playerController;
    private float _yAxisRotation;

    private float _gravityVelocity;

    public GameObject objectToPick;
    public bool isHandsBusy = false;

    void Start()
    {
        _playerController = GetComponent<CharacterController>();
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            PickUp(objectToPick);
        }
        RotateCamera();
        MovePlayer();
    }

    private void RotateCamera()
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"),Input.GetAxis("Mouse Y"));

        _currentMouseDelta = Vector2.SmoothDamp(_currentMouseDelta, targetMouseDelta,
            ref _currentMouseDeltaVelocity, mouseSmoothTime);

        _yAxisRotation -= _currentMouseDelta.y * mouseSensitivity;
        _yAxisRotation = Mathf.Clamp(_yAxisRotation, -90f, 90f);
        
        camera.localEulerAngles = Vector3.right * _yAxisRotation;
        
        transform.Rotate(Vector3.up * (_currentMouseDelta.x * mouseSensitivity));
    }

    private void MovePlayer()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        if (_playerController.isGrounded)
            _gravityVelocity = 0f;

        _gravityVelocity += gravity * Time.deltaTime;

        Vector3 velocity = (transform.forward * movement.z + transform.right * movement.x) * speed +
                           Vector3.up * _gravityVelocity;

        _playerController.Move(velocity * Time.deltaTime);
    }

    public void PickUp(GameObject objectToPickUp)
    {
        if (!isHandsBusy)
        {
            Instantiate(objectToPickUp, pickUpParent.position, pickUpParent.rotation, pickUpParent);
            isHandsBusy = true;
        }
        else
        {
            Destroy(pickUpParent.transform.GetChild(0).gameObject);
            Instantiate(objectToPickUp, pickUpParent.position, pickUpParent.rotation, pickUpParent);
        }
    }
}

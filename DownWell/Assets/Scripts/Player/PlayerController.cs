using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Parameters")]
    [SerializeField] private float _moveSpeed = 5f;         // WalkSpeed
    [SerializeField] private float _forceDash = 2.0f;
    [SerializeField] private float _jumpForce = 10.0f;


    [Header("Shoot Parameters")]
    [SerializeField] private GameObject _bullet;

    /*    [Header("Look Sensivity")]
        private float _mouseSensitivity = 2.0f;
        private float _upDownRange = 80.0f;*/
    //[SerializeField] private InputAction _playerControls;
    //private PlayerInputHandler _inputHandler;


    private Rigidbody2D _rb;
    private Vector2 _moveDirection = Vector2.zero;
    private Vector2 value;

    //private bool _dashing = false;


    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        //_rb.AddForce(_moveDirection * _moveSpeed * Time.fixedDeltaTime);
        Debug.Log(value);
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        // Vector 2 / analogique
        if (ctx.performed)
        {
            _moveDirection = ctx.ReadValue<Vector2>();
            _rb.AddForce(_moveDirection * _moveSpeed);
            Debug.Log("Move Perform");
        }
}

    public void OnJump(InputAction.CallbackContext ctx)
    {
        // Button

        if (ctx.performed)
        {
            _rb.AddForce(Vector2.up * _jumpForce);
            Debug.Log("Jump Perform");
        }
    }

    public void OnFire(InputAction.CallbackContext ctx)
    {
        // Button
        if (ctx.performed)
        {
            Vector3 spawnPostion = new Vector3(transform.position.x, transform.position.y + 10, transform.position.z);
            Instantiate(_bullet, spawnPostion, Quaternion.identity); // vitesse vers le bas
        }
    }

    public void OnDash(InputAction.CallbackContext ctx)
    {
        // Button

        if (ctx.performed)
        {
            _rb.AddForce(_moveDirection * _moveSpeed * _forceDash);
            Debug.Log("Dash Perform");
        }
    }
}
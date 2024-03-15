using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Parameters")]
    [SerializeField] private float _moveSpeed = 5f;         // WalkSpeed
    [SerializeField] private float _forceDash = 2.0f;
    [SerializeField] private float _jumpForce = 50.0f;


    [Header("Shoot Parameters")]
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _shootForce = 5.0f;

    /*    [Header("Look Sensivity")]
        private float _mouseSensitivity = 2.0f;
        private float _upDownRange = 80.0f;*/
    //[SerializeField] private InputAction _playerControls;
    //private PlayerInputHandler _inputHandler;


    private Rigidbody2D _rb;
    private Vector2 _moveDirection = Vector2.zero;
    private Vector2 value;

    private Vector2 _lastPos;

    //private bool _dashing = false;


    private void Start()
    {
        _lastPos = transform.position;
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CheckHorizontalDirection();
    }

    
    private void CheckHorizontalDirection()
    {
        float currentCamPosY = transform.position.y;
        //Debug.Log("difference camPos - lastPosCam : " + (currentCamPosY - _lastPos.y));
        
        if (Mathf.Abs(currentCamPosY - _lastPos.y) >= 5)
        {
            //StartCoroutine(GenerateWallsCoroutine());
            _lastPos = transform.position;
        }
    }


    public void OnMove(InputAction.CallbackContext ctx)
    {
        // Vector 2 / analogique
        _moveDirection = ctx.ReadValue<Vector2>();
        _rb.AddForce(_moveDirection * _moveSpeed, ForceMode2D.Impulse);
        //Debug.Log("Move Perform");
        
/*        if (ctx.performed)
        {
        }*/
}

    public void OnJump(InputAction.CallbackContext ctx)
    {
        // Button

        if (ctx.performed)
        {
            _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            Debug.Log("Jump Perform");
        }
    }

    public void OnFire(InputAction.CallbackContext ctx)
    {
        // Button
        if (ctx.performed)
        {
            Vector3 spawnPostion = new Vector3(transform.position.x, transform.position.y - 2, transform.position.z);
            Rigidbody2D bulletRb = Instantiate(_bullet, spawnPostion, Quaternion.identity).GetComponent<Rigidbody2D>(); // vitesse vers le bas
            bulletRb.AddForce(Vector2.down * -_shootForce, ForceMode2D.Impulse);
            
            _rb.AddForce(Vector2.up * _jumpForce / 2, ForceMode2D.Impulse);
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
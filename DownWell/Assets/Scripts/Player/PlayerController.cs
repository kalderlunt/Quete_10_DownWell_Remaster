using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Important Parameters")]
    public float hp = 100;
    public float hpMax = 100;
    public float damageAmount = 100;

    [Header("Movement Parameters")]
    [SerializeField] private float _moveSpeed = 5f;         // WalkSpeed
    [SerializeField] private float _forceDash = 2.0f;
    [SerializeField] private float _jumpForce = 50.0f;


    [Header("Shoot Parameters")]
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _shootForce = 5.0f;

    private Rigidbody2D _rb;
    
    private Collider2D _bodyCollider;
    [SerializeField] private Collider2D _feetCollider;

    private Vector2 _moveDirection = Vector2.zero;
    private Vector2 _lastPos;

    private bool _isMove = false;
    [HideInInspector] public bool canJump = false;
    //private bool _dashing = false;


    private void Start()
    {
        _lastPos = transform.position;
        
        _rb = GetComponent<Rigidbody2D>();
        _bodyCollider = GetComponent<Collider2D>();

        hp = hpMax;
    }

    private void Update()
    {
        CheckHorizontalDirection();
        UpdateMove();
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

    private void UpdateMove()
    {
        if (_isMove)
        {
            _rb.velocity = new Vector2(_moveDirection.x * _moveSpeed, _rb.velocity.y);
        }
    }


    public void OnMove(InputAction.CallbackContext ctx)
    {
        // Vector 2 / analogic
        if (ctx.started)
        {
            _isMove = true;
            _moveDirection = ctx.ReadValue<Vector2>();
        }

        if (ctx.canceled)
        {
             _isMove = false;
            _moveDirection = Vector2.zero;
        }
        
/*        if (ctx.performed)
        {
        }*/
    }

    public void JumpAction()
    {
        _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        // Button

        if (ctx.performed)
        {
            if (canJump)
            {
                canJump = false;
                JumpAction();
            }
        }
    }

    public void OnFire(InputAction.CallbackContext ctx)
    {
        // Button
        if (ctx.performed)
        {
            if (_rb.velocity.y < -_jumpForce)
            {
                _rb.velocity = new Vector2 (_rb.velocity.x, -_jumpForce);
            }

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
        }
    }


    /* /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\ */
    /* \/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/ */
    /* /\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\ */

    // Functions
}
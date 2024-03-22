using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [Header("Input Action Asset")]
    [SerializeField] private InputActionAsset _playerControls;

    [Header("Action Map Name References")]
    [SerializeField] private string _actionMapName;

    [Header("Action Name References")]
    [SerializeField] private string _move   =   "Move";
    [SerializeField] private string _look   =   "Look";
    [SerializeField] private string _jump   =   "Jump";
    [SerializeField] private string _fire   =   "Fire";
    [SerializeField] private string _dash   =   "Dash";

    private InputAction _moveAction;
    private InputAction _lookAction;
    private InputAction _jumpAction;
    private InputAction _fireAction;
    private InputAction _dashAction;

    private PlayerInput _playerInput;

    public Vector2  MoveInput       { get; private set; }
    public Vector2  LookInput       { get; private set; }
    public bool     JumpTriggered   { get; private set; }
    public bool     FireTriggered   { get; private set; }
    public bool     DashTriggered   { get; private set; }

    public static PlayerInputHandler Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        SetupInputActions();
        RegisterInputActions();
    }

    private void SetupInputActions()
    {
        _moveAction = _playerControls.FindActionMap(_actionMapName).FindAction(_move);
        _lookAction = _playerControls.FindActionMap(_actionMapName).FindAction(_look);
        _jumpAction = _playerControls.FindActionMap(_actionMapName).FindAction(_jump);
        _fireAction = _playerControls.FindActionMap(_actionMapName).FindAction(_fire);
        _dashAction = _playerControls.FindActionMap(_actionMapName).FindAction(_dash);

        //_dashAction = _playerInput.actions["Dash"];
    }

    private void RegisterInputActions()
    {
        _moveAction.performed   += context => MoveInput         = context.ReadValue<Vector2>();
        _moveAction.canceled    += context => MoveInput         = Vector2.zero;

        _lookAction.performed   += context => LookInput         = context.ReadValue<Vector2>();
        _lookAction.canceled    += context => LookInput         = Vector2.zero;

        _jumpAction.performed   += context => JumpTriggered     = true;
        _jumpAction.canceled    += context => JumpTriggered     = false;

        _fireAction.performed   += context => FireTriggered     = true;
        _fireAction.canceled    += context => FireTriggered     = false;

        //_sprintAction.performed += context => SprintValue = context.readvalue<float>();
        //_sprintAction.canceled += context => SprintValue = 0f;
    }

    private void OnEnable()
    {
        _moveAction.Enable();
        _lookAction.Enable();
        _jumpAction.Enable();
        _fireAction.Enable();
        _dashAction.Enable();
    }

    private void OnDisable()
    {
        _moveAction.Disable();
        _lookAction.Disable();
        _jumpAction.Disable();
        _fireAction.Disable();
        _dashAction.Disable();
    }
}
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Start Initiate player")]
    [SerializeField] private GameObject _player;
    [SerializeField] private Transform _spawnPlayerStart;

    [Header("Gravity")]
    [SerializeField] private float _timerBeforeTogglePhysics = 15.0f;
    public DirGravity gravityPower = DirGravity.DownWards; // 1 vers le bas || -1 vers le haut
    private float _timer = 0f;

    private void Awake()
    {
        Instantiate(_player, _spawnPlayerStart.position, Quaternion.identity);

        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject); 
        }

        //PrintDevices();
    }

    private void Start()
    {
        _timer = _timerBeforeTogglePhysics;
    }

    private void Update()
    {
        if (CheckGravityDirection() > DirGravity.Upwards) 
        {
            if (_timer <= 0)
                Physics2D.gravity *= -1;
            else
                _timer -= Time.deltaTime;
        }
        else
            if (_timer != _timerBeforeTogglePhysics)
                _timer = _timerBeforeTogglePhysics;
    }

    private DirGravity CheckGravityDirection()
    {
        if (Physics2D.gravity.y > 0)
        {
            gravityPower = DirGravity.Upwards;
            return gravityPower;
        }
        else
        {
            gravityPower = DirGravity.DownWards;
            return gravityPower;
        }
    }

    void PrintDevices()
    {
        foreach (var device in InputSystem.devices)
        {
            if (device.enabled)
                Debug.Log("Active Device : " + device.name);
        }
    }
}
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [Header("Start Initiate player")]
    [SerializeField] private GameObject _player;
    [SerializeField] private Transform _spawnPlayerStart;

    private void Awake()
    {
        //GameObject player =
        Instantiate(_player, _spawnPlayerStart.position, Quaternion.identity);


        //PrintDevices();
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
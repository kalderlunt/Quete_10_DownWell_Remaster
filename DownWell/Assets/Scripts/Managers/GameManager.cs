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

        //Instantiate();

/*        // Assurez-vous que la caméra existe et est attachée au joueur
        if (Camera.main != null)
        {
            // Rend la caméra un enfant du joueur
            Camera.main.transform.SetParent(player.transform, false);
        }*/

        PrintDevices();
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
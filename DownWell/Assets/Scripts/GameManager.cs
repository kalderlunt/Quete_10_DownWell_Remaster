using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Start Initiate player")]
    [SerializeField] private GameObject _player;
    [SerializeField] private Transform _spawnPlayerStart;

    private void Start()
    {
        GameObject player = Instantiate(_player, _spawnPlayerStart);
        //Camera.main.transform.parent = player.transform; 
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelManager : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject _prefabSpawnStart;
    [SerializeField] private GameObject _prefabLeftWall;
    [SerializeField] private GameObject _prefabRightWall;
    [SerializeField] private GameObject _blockPrefab;

    [SerializeField] private List<GameObject> _enemyList;


    [Header("Positions")]
    [SerializeField] private float _levelSize;
    private float _wallSpacingFromCenterInPixels = 0;
    [SerializeField, Range(1, 15)] private int _offsetSpawnPlatform = 7;

    private Transform _mainCam;
    private Vector3 _lastCamPos;
    private float _seed;


    private void Start()
    {
        _mainCam = Camera.main.transform;
        _wallSpacingFromCenterInPixels = Camera.main.orthographicSize / 2 + 1;
        _lastCamPos = _mainCam.position;

        //Debug.Log("_wallSpacingFromCenterInPixels : " + _wallSpacingFromCenterInPixels);

        GameObject platform = Instantiate(_prefabSpawnStart,  new Vector3(_mainCam.position.x / 2, _mainCam.position.y / 2 - _offsetSpawnPlatform, 0f), Quaternion.identity);
        platform.transform.parent = transform;

        _seed = Random.Range(-10000, 10000);
        _levelSize = _mainCam.position.y + _wallSpacingFromCenterInPixels - 5;

        GenerateWallsCoroutine();
    }

    private void Update()
    {
        CheckPos();
    }


    // Check if currentCamPos - lastCampos >= 5 
    // Generate New Walls coroutine

    private void CheckPos()
    {
        float currentCamPosY = _mainCam.position.y;
        //Debug.Log("difference camPos - lastPosCam : " + (currentCamPosY - _lastCamPos.y));
        if (Mathf.Abs(currentCamPosY - _lastCamPos.y) >= 5)
        {
            GenerateWallsCoroutine();
            //StartCoroutine(GenerateWallsCoroutine());
            
            StartCoroutine(GeneratePlatforms());
            _lastCamPos = _mainCam.position;
        }
    }

    private void GenerateWallsCoroutine()
    {
        float cameraBottomY = _mainCam.position.y - Camera.main.orthographicSize;
        float currentY = Mathf.RoundToInt(_mainCam.position.y);

        while (currentY > cameraBottomY - 10)
        {
            Vector3 blockPosition = new Vector3(_mainCam.position.x / 2, currentY, 0f);

            // Vérifie s'il y a déjà un bloc à l'emplacement
            Collider2D existingBlock = Physics2D.OverlapCircle(blockPosition, 0.1f);
            if (existingBlock == null)
            {
                GameObject leftWall     = Instantiate(_prefabLeftWall,  new Vector3(_mainCam.position.x / 2 - _wallSpacingFromCenterInPixels /* /100*/, currentY, 0f), Quaternion.identity);
                GameObject rightWall    = Instantiate(_prefabRightWall, new Vector3(_mainCam.position.x / 2 + _wallSpacingFromCenterInPixels /* /100*/, currentY, 0f), Quaternion.identity);

                leftWall.transform.parent  = transform;
                rightWall.transform.parent = transform;
            }

            currentY -= 1f;  // - 1 cube size

            //yield return null;
        }
    }

    private IEnumerator GeneratePlatforms()
    {
        float cameraBottomY = _mainCam.position.y - Camera.main.orthographicSize;
        Vector3Int spawnPos = new Vector3Int((int)(-_wallSpacingFromCenterInPixels + 1), (int)cameraBottomY, 0);
        
        for (int i = 0; i < _levelSize; i++) // position y
        {
            for (int y = 0; y < (int)Camera.main.orthographicSize - 1; y++) // position x
            {
                float spawnProbSub = 0;
                float screenPercent = 0.0f;
                float lessBlockMidPercent = 0.15f;

                if (Mathf.Abs(spawnPos.x) < Camera.main.orthographicSize * screenPercent) 
                    spawnProbSub += lessBlockMidPercent;

                if (Mathf.PerlinNoise(spawnPos.x / 10f + _seed, spawnPos.y / 10f + _seed) > 0.70f + spawnProbSub) // 0.70f = % de remplissage (noir)
                {
                    Instantiate(_blockPrefab, spawnPos, Quaternion.identity);
                }
                else
                {
                    GenerateEnemy(spawnPos);
                }
                spawnPos.x += 1;
            }
            spawnPos.x = (int)(-Camera.main.orthographicSize / 2) + 1;
            spawnPos.y -= 1;
        }
        yield return new WaitForSeconds(0.001f);
    }

    private void GenerateEnemy(Vector3Int spawnPos)
    {
        float randomValue = Random.Range(0f, 100f);
        if (randomValue < 0.5f)
        {
            Instantiate(_enemyList[Random.Range(0, _enemyList.Count)], spawnPos, Quaternion.identity);
        }
    }
}
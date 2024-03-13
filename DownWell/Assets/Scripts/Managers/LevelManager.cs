using System.Collections;
using UnityEditor.Overlays;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject _prefabLeftWall;
    [SerializeField] private GameObject _prefabRightWall;
    [SerializeField] private GameObject _blockPrefab;

    [SerializeField, Range(400f, 600f)] private float _wallSpacingFromCenterInPixels = 500f;

    private Transform _mainCam;

    private bool _finisheEarlyAnimation = false;

    private void Start()
    {
        _mainCam = Camera.main.transform;

        StartCoroutine(GenerateWallsCoroutine());
    }

    private void Update()
    {
        GenerateWalls();
    }

    private void GenerateWalls()
    {
        if (_finisheEarlyAnimation)
        {
            float cameraBottomY = _mainCam.position.y - Camera.main.orthographicSize;
            float currentY = _mainCam.position.y;

            while (currentY > cameraBottomY - 10)
            {
                Vector3 blockPosition = new Vector3(_mainCam.position.x / 2, currentY, 0f);

                // Vérifie s'il y a déjà un bloc à l'emplacement
                Collider2D existingBlock = Physics2D.OverlapCircle(blockPosition, 0.1f);
                if (existingBlock == null)
                {
                    GameObject leftWall     = Instantiate(_prefabLeftWall,  new Vector3(_mainCam.position.x / 2 - _wallSpacingFromCenterInPixels / 100, currentY, 0f), Quaternion.identity);
                    GameObject rightWall    = Instantiate(_prefabRightWall, new Vector3(_mainCam.position.x / 2 + _wallSpacingFromCenterInPixels / 100, currentY, 0f), Quaternion.identity);

                    leftWall.transform.parent = transform;
                    rightWall.transform.parent = transform;
                }
                currentY -= 1f;  // - 1 cube size
            }
        }
    }

    private IEnumerator GenerateWallsCoroutine()
    {
        float cameraBottomY = _mainCam.position.y - Camera.main.orthographicSize;
        float currentY = _mainCam.position.y;

        while (currentY > cameraBottomY - 10)
        {
            Vector3 blockPosition = new Vector3(_mainCam.position.x / 2, currentY, 0f);

            // Vérifie s'il y a déjà un bloc à l'emplacement
            Collider2D existingBlock = Physics2D.OverlapCircle(blockPosition, 0.1f);
            if (existingBlock == null)
            {
                GameObject leftWall     = Instantiate(_prefabLeftWall,  new Vector3(_mainCam.position.x / 2 - _wallSpacingFromCenterInPixels / 100, currentY, 0f), Quaternion.identity);
                GameObject rightWall    = Instantiate(_prefabRightWall, new Vector3(_mainCam.position.x / 2 + _wallSpacingFromCenterInPixels / 100, currentY, 0f), Quaternion.identity);

                leftWall.transform.parent  = transform;
                rightWall.transform.parent = transform;
            }

            currentY -= 1f;  // - 1 cube size

            yield return new WaitForSeconds(0.05f);
        }

        float cameraTopY = _mainCam.position.y + Camera.main.orthographicSize;
        while (currentY > cameraTopY + 10)
        {
            Vector3 blockPosition = new Vector3(_mainCam.position.x / 2, currentY, 0f);

            // Vérifie s'il y a déjà un bloc à l'emplacement
            Collider2D existingBlock = Physics2D.OverlapCircle(blockPosition, 0.1f);
            if (existingBlock == null)
            {
                GameObject leftWall     = Instantiate(_prefabLeftWall,  new Vector3(_mainCam.position.x / 2 - _wallSpacingFromCenterInPixels / 100, currentY, 0f), Quaternion.identity);
                GameObject rightWall    = Instantiate(_prefabRightWall, new Vector3(_mainCam.position.x / 2 + _wallSpacingFromCenterInPixels / 100, currentY, 0f), Quaternion.identity);

                leftWall.transform.parent  = transform;
                rightWall.transform.parent = transform;
            }

            currentY += 1f;  // - 1 cube size

            yield return new WaitForSeconds(0.05f);
        }
        _finisheEarlyAnimation = true;
    }

    private IEnumerator GenerateBlocksCoroutine()
    {
        while (true)
        {
            Vector3 blockPosition = new Vector3(Random.Range(_mainCam.position.x / 2 - _wallSpacingFromCenterInPixels / 100, _mainCam.position.x / 2 + _wallSpacingFromCenterInPixels / 100), _mainCam.position.y, 0f);
            
            Instantiate(_blockPrefab, blockPosition, Quaternion.identity);

            yield return new WaitForSeconds(1f);
        }
    }
}
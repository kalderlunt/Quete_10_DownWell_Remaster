using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject _prefabLeftWall;
    [SerializeField] private GameObject _prefabRightWall;
    [SerializeField] private GameObject _blockPrefab;

    [SerializeField, Range(400f, 600f)] private float _wallSpacingFromCenterInPixels = 500f;

    private Transform _mainCam;

    private void Start()
    {
        _mainCam = Camera.main.transform;

        StartCoroutine(GenerateWallsCoroutine());
        StartCoroutine(GenerateBlocksCoroutine());
    }

    private IEnumerator GenerateWallsCoroutine()
    {   
        while(true)
        {
            float cameraBottomY = _mainCam.position.y - Camera.main.orthographicSize;
            float currentY = _mainCam.position.y;

                GameObject leftWall     = Instantiate(_prefabLeftWall,  new Vector3(_mainCam.position.x / 2 - _wallSpacingFromCenterInPixels / 100 ,    currentY, 0f), Quaternion.identity);
                GameObject rightWall    = Instantiate(_prefabRightWall, new Vector3(_mainCam.position.x / 2 + _wallSpacingFromCenterInPixels / 100,     currentY, 0f), Quaternion.identity);

                leftWall.transform.parent = transform;
                rightWall.transform.parent = transform;

                currentY -= 1f;  // - 1 cube size

                yield return new WaitForSeconds(0.1f);
            /*while (currentY > cameraBottomY - 10)
            {
            }*/
        }
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
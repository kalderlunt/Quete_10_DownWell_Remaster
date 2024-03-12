using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject _prefabLeftWall;
    [SerializeField] private GameObject _prefabRightWall;

    [SerializeField , Range(200f, 500f)] private float _wallSpacingFromCenterInPixels = 100f;

    private Transform _mainCam;

    private void Start()
    {
        _mainCam = Camera.main.transform;


        /*for (int i = 0; i < 5; i++)
        {
            Instantiate(_prefabLeftWall, new Vector3(((_mainCam.position.x / 2) - (_wallSpacingFromCenterInPixels / 100f)), i, 0f), Quaternion.identity); // je divise par 100 pour transformer _wallSpacingFromCenterInPixels en px

            Instantiate(_prefabLeftWall, new Vector3(((_mainCam.position.x / 2) + (_wallSpacingFromCenterInPixels / 100f)), i, 0f), Quaternion.identity); // je divise par 100 pour transformer _wallSpacingFromCenterInPixels en px
        }*/
    }

    private void Update()
    {
        /*float cameraBottomY = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane)).y;

        float currentY = cameraBottomY + _wallSpacingFromCenterInPixels / 200f; // Utilise la moitié de la taille verticale d'un cube
        if (currentY < _mainCam.position.y)
        {
            // Instantie les murs dans le parent du GameObject
            GameObject leftWall = Instantiate(_prefabLeftWall, new Vector3(_mainCam.position.x / 2 - _wallSpacingFromCenterInPixels / 100f, _mainCam.position.y + currentY, 0f), Quaternion.identity);
            GameObject rightWall = Instantiate(_prefabRightWall, new Vector3(_mainCam.position.x / 2 + _wallSpacingFromCenterInPixels / 100f, _mainCam.position.y + currentY, 0f), Quaternion.identity);

            // Affecte le parent aux murs
            leftWall.transform.parent = transform;
            rightWall.transform.parent = transform;

            currentY += _wallSpacingFromCenterInPixels / 100f; // Incrémente la position Y en fonction de l'espacement des murs
        }*/


        float cameraBottomY = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane)).y;

        float currentY = cameraBottomY + _wallSpacingFromCenterInPixels / 200f; // Utilise la moitié de la taille verticale d'un cube
        if (currentY < _mainCam.position.y)
        {
            GameObject leftWall = Instantiate(_prefabLeftWall, new Vector3(_mainCam.position.x / 2 - _wallSpacingFromCenterInPixels / 100f, currentY, 0f), Quaternion.identity);
            GameObject rightWall = Instantiate(_prefabRightWall, new Vector3(_mainCam.position.x / 2 + _wallSpacingFromCenterInPixels / 100f, currentY, 0f), Quaternion.identity);

            leftWall.transform.parent = transform;
            rightWall.transform.parent = transform;

            currentY += _wallSpacingFromCenterInPixels / 100f; // Incrémente la position Y en fonction de l'espacement des murs
        }

    }
}
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Transform _target; // Target => player
    private Transform _mainCam;
    private float _depthCam = 5.0f;    // Vitesse descente

    [SerializeField, Range(0.0f, 1.0f)] private float _smoothSpeedDown  = 0.1f;    // Vitesse descente
    //[SerializeField] private float smoothSpeedUp    = 6.0f;       // Vitesse montée



    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        _mainCam = Camera.main.transform;

        if (player != null)
            _target = player.transform;
        else
            Debug.LogError("Player not found. Make sure your player GameObject is tagged as 'Player'.");
    }



    private void Update()
    {
        if (_target != null)
            _mainCam.transform.position = new Vector3(0.0f, _mainCam.transform.position.y * (1 - _smoothSpeedDown) + _target.position.y * _smoothSpeedDown, -_depthCam);
        else
            Debug.LogWarning("Target not set for CameraManager. Make sure the player is tagged as 'Player' in the scene.");
    }
}


// Current camera position
//Vector3 desiredPosition = new Vector3(_mainCam.transform.position.x, _target.position.y, _mainCam.transform.position.z);

// Choose speed value depending on direction
//float currentSmoothSpeed = (_target.position.y > _mainCam.transform.position.y) ? smoothSpeedUp : smoothSpeedDown;

// Interpole la position actuelle de la caméra vers la position désirée de manière fluide
//Vector3 smoothedPosition = Vector3.Lerp(_mainCam.transform.position, desiredPosition, currentSmoothSpeed * Time.deltaTime);
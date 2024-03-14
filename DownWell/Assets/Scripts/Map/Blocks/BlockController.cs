using UnityEngine;

public class BlockController : MonoBehaviour
{
    private Transform _mainCam;

    private void Start()
    {
        _mainCam = Camera.main.transform;
    }

    private void Update()
    {
        float cameraTopY = _mainCam.position.y + Camera.main.orthographicSize;

        // V�rifie si le bloc est hors de l'�cran + 10 unit�s en Y
        if (transform.position.y > cameraTopY + 10)
        {
            //Debug.Log("destroyyyyyyyyyyyyy");
            Destroy(gameObject);
        }
    }
}
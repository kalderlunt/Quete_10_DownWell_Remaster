using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public float hp = 100;
    public float hpMax = 100;

    [SerializeField] private float _percentToGiveSkills = 100.0f;
    [SerializeField] private List<GameObject> _skillsPrefab;

    private Transform _mainCam;

    private void Start()
    {
        _mainCam = Camera.main.transform;
        hp = hpMax;
    }

    private void Update()
    {
        float cameraTopY = _mainCam.position.y + Camera.main.orthographicSize;

        // Vérifie si le bloc est hors de l'écran + 10 unités en Y
        if (transform.position.y > cameraTopY + 10)
        {
            if (gameObject != null) 
                Destroy(gameObject);
        }

        if (hp <= 0)
        {
            Vector3 positionblock = transform.position;
            Destroy(gameObject);

            if (gameObject == null)
            {
                float percentSpawnBonus = Random.Range(0f, 100f);
                int indexSkill = Random.Range(0, _skillsPrefab.Count);

                if (percentSpawnBonus /100 <= _percentToGiveSkills / 100)
                {
                    Instantiate(_skillsPrefab[indexSkill], positionblock, Quaternion.identity);
                }
            }
        }
    }
}
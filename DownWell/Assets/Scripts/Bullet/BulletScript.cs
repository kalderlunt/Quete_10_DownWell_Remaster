using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float _lerpSpeed = 0.5f; 
    [SerializeField] private Vector3 _targetScale = new Vector3(0.1f, 0.1f, 0.1f);

    [Header("Colliders")]
    //[SerializeField] private Collider2D _collider;
    private PlayerController _playerController;

    private void Start()
    {
        _playerController = GameObject.FindWithTag(Tag.Player.ToString()).GetComponent<PlayerController>();
    }

    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, _targetScale, _lerpSpeed * Time.deltaTime);

        if (transform.localScale.x <= 0.1f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //if (_collider != null)
        //{
            if (other.collider.transform.parent.parent.CompareTag(Tag.Breakable.ToString()))
            {
                Destroy(gameObject);
                Destroy(other.transform.parent.parent.gameObject);
            }

            if (other.collider.transform.parent.parent.CompareTag(Tag.Enemy.ToString()))
            {
                EnemyScript enemyScript = other.transform.parent.parent.GetComponent<EnemyScript>();
                enemyScript.hp -= _playerController.damageAmount;
                
                Destroy(gameObject);
            }
        //}
    }
}
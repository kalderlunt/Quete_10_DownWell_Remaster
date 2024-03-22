using UnityEngine;

public class PlayerBody : MonoBehaviour
{
    private PlayerController _playerController;

    private void Start()
    {
        _playerController = transform.parent.parent.GetComponent<PlayerController>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform != null && other.transform.parent != null && other.transform.parent.parent != null)
        {
            if (other.transform.parent.parent.CompareTag(Tag.Enemy.ToString()))
            {
                EnemyScript enemyScript = other.transform.parent.parent.GetComponent<EnemyScript>();
                _playerController.hp -= enemyScript.damageAmount;
            }
        }
    }
}
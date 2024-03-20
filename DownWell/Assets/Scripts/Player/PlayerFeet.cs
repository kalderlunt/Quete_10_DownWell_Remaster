using UnityEngine;

public class PlayerFeet : MonoBehaviour
{
    private PlayerController _playerController;

    private void Start()
    {
        _playerController = transform.parent.parent.GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D other) // je voudrais que ce soit les _feet qui font la detection
    {
        if (other.transform.parent.parent.CompareTag(Tag.Floor.ToString()) || other.transform.parent.parent.CompareTag(Tag.Breakable.ToString()))
        {
            _playerController.canJump = true;
        }

        if (other.transform.parent.parent.CompareTag(Tag.Enemy.ToString()))
        {
            _playerController.JumpAction();
        }
    }
}
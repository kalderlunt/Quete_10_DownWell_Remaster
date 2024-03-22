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
        if (other.transform.parent.parent.CompareTag(Tag.Enemy.ToString()))
        {
            Destroy(other.transform.parent.parent.gameObject);
            _playerController.JumpAction();
        }

        if (other.transform.parent.parent.CompareTag(Tag.Floor.ToString()) || other.transform.parent.parent.CompareTag(Tag.Breakable.ToString()))
        {
            _playerController.canJump = true;
        }

        if (other.transform.parent.parent.CompareTag(Tag.Events.ToString()))
        {
            EventGravity eventGravity = other.transform.parent.parent.GetComponent<EventGravity>();
            
            switch (eventGravity.eventId)
            {
                case EventType.Gravity:
                    eventGravity.ToggleGravity();
                    break;
                
                default:
                    break;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _playerController.canJump = false;
    }
}
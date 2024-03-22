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
            GravityEvent gravityEvent = other.transform.parent.parent.GetComponent<GravityEvent>();
            gravityEvent.Execute();
            Destroy(other.transform.parent.parent.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _playerController.canJump = false;
    }
}


/*            GravityEvent gravityEvent = other.transform.parent.parent.GetComponent<GravityEvent>();
            
            switch (gravityEvent.eventId)
            {
                case EventType.Gravity:
                    eventGravity.ToggleGravity();
                    break;
                
                default:
                    break;
            }*/
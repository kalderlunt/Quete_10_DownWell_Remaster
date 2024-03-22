using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class EventGravity : MonoBehaviour
{
    public EventType eventId = EventType.Gravity;
    private PlayerController _playerController;


    private void Start()
    {
        _playerController = GameObject.FindWithTag(Tag.Player.ToString()).GetComponent<PlayerController>();
    }

    public void ToggleGravity()
    {
        Physics2D.gravity *= -1;

        StartCoroutine(PlayerRotation());
    }

    private IEnumerator PlayerRotation()
    {
        float rotationDuration = 1.0f;
        float elapsedRotationTime = 0f;

        Vector3 currentRotation = _playerController.transform.rotation.eulerAngles;
        float targetZRotation = (currentRotation.z * Mathf.Deg2Rad + Mathf.PI) % (2 * Mathf.PI);

        Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetZRotation * Mathf.Rad2Deg);

        while (elapsedRotationTime <= rotationDuration)
        {
            _playerController.transform.rotation = Quaternion.Slerp(_playerController.transform.rotation, targetRotation, elapsedRotationTime / rotationDuration);

            elapsedRotationTime += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
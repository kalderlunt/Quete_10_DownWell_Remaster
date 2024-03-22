using System.Collections;
using UnityEngine;

public class GravityEvent : EventSystem
{
    public override void Execute()
    {
        ToggleGravity();
    }

    private void ToggleGravity()
    {
        PlayerController playerController = GameObject.FindWithTag(Tag.Player.ToString()).GetComponent<PlayerController>();
        if (playerController != null)
        {
            if (GameManager.instance.gravityPower == DirGravity.DownWards)
                GameManager.instance.gravityPower = DirGravity.Upwards;
            else
                GameManager.instance.gravityPower = DirGravity.DownWards;

            Physics2D.gravity *= -1;
            playerController.StartCoroutine(PlayerRotation(playerController));
        }
        else
        {
            Debug.LogError("PlayerController not found.");
        }
    }

    private IEnumerator PlayerRotation(PlayerController playerController)
    {
        float rotationDuration = 1.0f;
        float elapsedRotationTime = 0f;

        Vector3 currentRotation = playerController.transform.rotation.eulerAngles;
        float targetZRotation = (currentRotation.z * Mathf.Deg2Rad + Mathf.PI) % (2 * Mathf.PI);

        Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetZRotation * Mathf.Rad2Deg);

        Debug.Log("ROTATION, elapsedRotationTime: " + elapsedRotationTime);
        while (elapsedRotationTime <= rotationDuration)
        {
            playerController.transform.rotation = Quaternion.Slerp(playerController.transform.rotation, targetRotation, elapsedRotationTime / rotationDuration);

            elapsedRotationTime += Time.deltaTime;
            yield return null;
        }
    }
}
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private Renderer playerRenderer;
    [SerializeField] private float flashLength;
    [SerializeField] private PlayerPickup playerPickup;
    private float flashCounter;

    private void Update()
    {
        flashCounter -= Time.deltaTime;
        if (flashCounter > 0)
        {
            playerRenderer.enabled = !playerRenderer.enabled;
            flashCounter = flashLength;
        }
        else
        {
            playerRenderer.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        playerTransform.transform.position = respawnPoint.position;
        flashCounter = flashLength;
        playerPickup.ResetLaps();
    }
}

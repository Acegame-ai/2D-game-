using UnityEngine;

public class FirerateIncreaser : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.IncreaseFireRate(); // Implement this method in PlayerController
                Destroy(gameObject);
            }
            Debug.Log("Fire rate increased");
        }
    }
}

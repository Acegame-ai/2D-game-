using UnityEngine;

public class Morebullets : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.AddMoreBullets(); // Implement this method in PlayerController
                Destroy(gameObject);
            }
            Debug.Log("Bullets added");
        }
    }
}

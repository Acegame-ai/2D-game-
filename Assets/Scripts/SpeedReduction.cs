using UnityEngine;

public class SpeedReduction : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.ReduceSpeed(); // Implement this method in PlayerController
                Destroy(gameObject);
            }
            Debug.Log("Speed Reduced");
        }
    }
}

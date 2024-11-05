using System.Collections;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public GameObject firerateIncreaserPrefab;
    public GameObject moreBulletsPrefab;
    public GameObject increasedDamagePrefab;
    public GameObject speedReductionPrefab;

    public float spawnInterval = 5f; // Time between power-up spawns
    public float powerUpDuration = 15f; // Duration of each power-up

    private void Start()
    {
        StartCoroutine(SpawnPowerUps());
    }

    private IEnumerator SpawnPowerUps()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            // Randomly select a power-up to spawn
            int powerUpType = Random.Range(0, 4); // 0-3 for 4 types

            GameObject powerUp = null;
            switch (powerUpType)
            {
                case 0:
                    powerUp = Instantiate(firerateIncreaserPrefab, GetRandomPosition(), Quaternion.identity);
                    break;
                case 1:
                    powerUp = Instantiate(moreBulletsPrefab, GetRandomPosition(), Quaternion.identity);
                    break;
                case 2:
                    powerUp = Instantiate(increasedDamagePrefab, GetRandomPosition(), Quaternion.identity);
                    break;
                case 3:
                    powerUp = Instantiate(speedReductionPrefab, GetRandomPosition(), Quaternion.identity);
                    break;
            }

            if (powerUp != null)
            {
                Destroy(powerUp, 8f); // Destroy power-up after 8 seconds
            }
        }
    }

    private Vector2 GetRandomPosition()
    {
        // Adjust these values to fit your game area
        float x = Random.Range(-10f, 10f);
        float y = Random.Range(-10f, 10f);
        return new Vector2(x, y);
    }
}

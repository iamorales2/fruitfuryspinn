using UnityEngine;
using System.Collections;

public class BouncingFruitSpawner : MonoBehaviour
{
    // Spawn points grouped by area
    public Transform[] topSpawnPoints;
    public Transform[] leftSpawnPoints;
    public Transform[] rightSpawnPoints;
    public Transform[] bottomSpawnPoints;

    // Fruit prefabs for each area
    public GameObject[] topFruitPrefabs;
    public GameObject[] leftFruitPrefabs;
    public GameObject[] rightFruitPrefabs;
    public GameObject[] bottomFruitPrefabs;

    public Transform target;             // Target for fruits to move towards
    public float bpm = 120f;             // Beats per minute for spawn timing

    private float timeBetweenSpawns;     // Time interval between each spawn

    private void Start()
    {
        timeBetweenSpawns = 60f / bpm;   // Calculate spawn interval from BPM
        StartCoroutine(SpawnFruitAtIntervals());
    }

    private IEnumerator SpawnFruitAtIntervals()
    {
        while (true)
        {
            // Wait for the next spawn interval
            yield return new WaitForSeconds(timeBetweenSpawns);

            // Randomly pick an area (top, left, right, or bottom)
            int area = Random.Range(0, 4); // 0 = top, 1 = left, 2 = right, 3 = bottom

            // Spawn a fruit based on the selected area
            if (area == 0)
            {
                SpawnFruitFromArea(topSpawnPoints, topFruitPrefabs);
            }
            else if (area == 1)
            {
                SpawnFruitFromArea(leftSpawnPoints, leftFruitPrefabs);
            }
            else if (area == 2)
            {
                SpawnFruitFromArea(rightSpawnPoints, rightFruitPrefabs);
            }
            else
            {
                SpawnFruitFromArea(bottomSpawnPoints, bottomFruitPrefabs);
            }
        }
    }

    private void SpawnFruitFromArea(Transform[] spawnPoints, GameObject[] fruitPrefabs)
    {
        // Randomly select a spawn point from the given area
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[spawnIndex];

        // Randomly select a fruit prefab from the given area's fruit pool
        int fruitIndex = Random.Range(0, fruitPrefabs.Length);
        GameObject selectedFruit = fruitPrefabs[fruitIndex];

        // Instantiate the fruit at the selected spawn point
        GameObject fruit = Instantiate(selectedFruit, spawnPoint.position, Quaternion.identity);

        // Assign the target to the fruit's movement script
        BouncingFruitMovement fruitMovement = fruit.GetComponent<BouncingFruitMovement>();
        if (fruitMovement != null)
        {
            fruitMovement.target = target;
        }
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnderwaterHazard2D : MonoBehaviour
{
    public GameObject rockPrefab;
    public GameObject seaweedPrefab;
    public int numberOfRocks = 5;
    public int numberOfSeaweed = 3;
    public float rockFallChance = 0.2f;
    public float seaweedGrowthRate = 0.3f;
    public float waveStrength = 2f;
    public float waveFrequency = 3f;
    public float entangleDuration = 2f;

    private List<GameObject> spawnedRocks = new List<GameObject>();
    private List<GameObject> spawnedSeaweed = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(GenerateWavyCurrents());
        SpawnRocks();
        SpawnSeaweed();
    }

    private void Update()
    {
        CheckForFallingRocks();
        GrowSeaweed();
    }

    void SpawnRocks()
    {
        for (int i = 0; i < numberOfRocks; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-8f, 8f), Random.Range(4f, 6f), 0);
            GameObject rock = Instantiate(rockPrefab, spawnPosition, Quaternion.identity);
            spawnedRocks.Add(rock);
            DestroyAfterTime(rock, Random.Range(5f, 10f)); // Rocks disappear after 5-10 seconds
        }
    }

    void SpawnSeaweed()
    {
        for (int i = 0; i < numberOfSeaweed; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-8f, 8f), -4f, 0);
            GameObject seaweed = Instantiate(seaweedPrefab, spawnPosition, Quaternion.identity);
            spawnedSeaweed.Add(seaweed);
            DestroyAfterTime(seaweed, Random.Range(8f, 15f)); // Seaweed disappears after 8-15 seconds
        }
    }

    IEnumerator GenerateWavyCurrents()
    {
        while (true)
        {
            float waveForceX = Mathf.Sin(Time.time * waveFrequency) * waveStrength;
            float waveForceY = Mathf.Cos(Time.time * waveFrequency) * waveStrength;

            GameObject waveZone = new GameObject("WaveZone");
            waveZone.transform.position = new Vector3(Random.Range(-8f, 8f), Random.Range(-4f, 4f), 0);
            DestroyAfterTime(waveZone, Random.Range(3f, 7f)); // Waves disappear after 3-7 seconds

            foreach (GameObject rock in spawnedRocks)
            {
                rock.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(waveForceX, waveForceY);
            }

            foreach (GameObject seaweed in spawnedSeaweed)
            {
                seaweed.transform.position += new Vector3(waveForceX * Time.deltaTime, 0, 0);
            }

            yield return new WaitForSeconds(Random.Range(2f, 5f)); // Change wave patterns every few seconds
        }
    }

    void CheckForFallingRocks()
    {
        foreach (GameObject rock in spawnedRocks)
        {
            if (Random.value < rockFallChance * Time.deltaTime)
            {
                StartCoroutine(FallRock(rock));
            }
        }
    }

    IEnumerator FallRock(GameObject rock)
    {
        yield return new WaitForSeconds(Random.Range(1f, 3f));

        Rigidbody2D rb = rock.GetComponent<Rigidbody2D>();
        if (rb == null) rb = rock.AddComponent<Rigidbody2D>();

        rb.gravityScale = 2f;
    }

    void GrowSeaweed()
    {
        foreach (GameObject seaweed in spawnedSeaweed)
        {
            seaweed.transform.localScale += new Vector3(0, seaweedGrowthRate * Time.deltaTime, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (GameObject seaweed in spawnedSeaweed)
            {
                if (Vector3.Distance(other.transform.position, seaweed.transform.position) < 1.5f)
                {
                    StartCoroutine(EntanglePlayer(other.gameObject));
                }
            }
        }
    }

    IEnumerator EntanglePlayer(GameObject player)
    {
        PlayerController2D controller = player.GetComponent<PlayerController2D>();
        controller.enabled = false; // Freeze movement

        yield return new WaitForSeconds(entangleDuration);

        controller.enabled = true; // Restore movement
    }

    void DestroyAfterTime(GameObject obj, float time)
    {
        Destroy(obj, time);
    }
}
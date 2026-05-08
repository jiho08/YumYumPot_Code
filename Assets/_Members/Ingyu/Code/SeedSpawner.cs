using UnityEngine;
using System.Collections;

[System.Serializable]
public class SeedData
{
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    [Range(0f, 50f)]
    private float chance = 0;

    public float weight { get; set; }
    public GameObject Prefab => prefab;
    public float Chance => chance;
}

public class SeedSpawner : MonoBehaviour
{
    [SerializeField] private SeedData[] seeds;

    [SerializeField]
    [Range(0, 500)]
    private int maxSeedCount = 100;

    [SerializeField]
    private Vector2 min = new Vector2(-8f, -4f);
    [SerializeField]
    private Vector2 max = new Vector2(8f, 4f);

    private float accumulatedWeights;

    private void Awake()
    {
        CalculateWeights();
    }

    private IEnumerator Start()
    {
        int count = 0;

        while (count < maxSeedCount)
        {
            SpawnSeed(new Vector2(
                Random.Range(min.x, max.x),
                Random.Range(min.y, max.y)));

            count++;
            yield return new WaitForSeconds(0.01f);
        }
    }

    private void CalculateWeights()
    {
        accumulatedWeights = 0f;

        foreach (var seed in seeds)
        {
            accumulatedWeights += seed.Chance;
            seed.weight = accumulatedWeights;
        }
    }

    private void SpawnSeed(Vector2 position)
    {
        var seed = seeds[GetRandomIndex()];
        Instantiate(seed.Prefab, position, Quaternion.identity);
    }

    private int GetRandomIndex()
    {
        float random = Random.value * accumulatedWeights;

        for (int i = 0; i < seeds.Length; i++)
        {
            if (seeds[i].weight >= random)
                return i;
        }

        return 0;
    }
}

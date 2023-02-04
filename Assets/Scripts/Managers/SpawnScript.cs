using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnScript : MonoBehaviour
{
    public List<GameObject> badPrefabs = new();
    public List<GameObject> goodPrefabs = new();
    private float timeSinceLastSpawn = 0.0f;
    [SerializeField]
    private Transform spawnParent;

    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= GameRandom.Core.NextFloat(2.0f, 3.1f))
        {
            timeSinceLastSpawn = 0.0f;
            SpawnObject(badPrefabs[Random.Range(0,badPrefabs.Count)]);
            SpawnObject(goodPrefabs[Random.Range(0,goodPrefabs.Count)]);
        }
    }

    void SpawnObject(GameObject prefabToSpawn)
    {
        Vector2 randomPos = new Vector2(GameRandom.Core.NextFloat(-8.0f, 8.0f),  -spawnParent.transform.position.y - 15);
        prefabToSpawn.transform.position = randomPos;
        prefabToSpawn.transform.rotation = transform.rotation;
        prefabToSpawn.transform.localScale = transform.localScale;
        Instantiate(prefabToSpawn, spawnParent);
    }
}
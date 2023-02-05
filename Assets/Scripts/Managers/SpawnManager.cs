using UnityEngine;
using UnityEngine.Pool;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private Transform spawnedParent;
    [SerializeField]
    private PooledGameObject[] powerDownPrefabs;
    [SerializeField]
    private PooledGameObject[] powerUpPrefabs;
    [SerializeField]
    private float powerDownSpawnTimeMin = 0.5f;
    [SerializeField]
    private float powerDownSpawnTimeMax = 3f;
    [SerializeField]
    private float powerUpSpawnTimeMin = 2f;
    [SerializeField]
    private float powerUpSpawnTimeMax = 3f;

    private float powerDownTimeSinceLastSpawn;
    private float powerUpTimeSinceLastSpawn;

    private IObjectPool<PooledGameObject> powerDownSpawnPool;
    private IObjectPool<PooledGameObject> powerUpSpawnPool;

    private void Awake()
    {
        powerDownSpawnPool = new ObjectPool<PooledGameObject>(
            CreatePooledPowerDown,
            OnTakeFromPoolPowerDown,
            OnReturnedToPool,
            OnDestroyPoolObject);

        powerUpSpawnPool = new ObjectPool<PooledGameObject>(
            CreatePooledPowerUp,
            OnTakeFromPoolPowerUp,
            OnReturnedToPool,
            OnDestroyPoolObject);
    }

    private void Update()
    {
        powerDownTimeSinceLastSpawn += Time.deltaTime;

        if (powerDownTimeSinceLastSpawn >= GameRandom.Core.NextFloat(powerDownSpawnTimeMin, powerDownSpawnTimeMax))
        {
            powerDownTimeSinceLastSpawn = 0f;
            powerDownSpawnPool.Get();
        }

        powerUpTimeSinceLastSpawn += Time.deltaTime;

        if (powerUpTimeSinceLastSpawn >= GameRandom.Core.NextFloat(powerUpSpawnTimeMin, powerUpSpawnTimeMax))
        {
            powerUpTimeSinceLastSpawn = 0f;
            powerUpSpawnPool.Get();
        }
    }

    private PooledGameObject CreatePooledPowerDown()
    {
        return CreatePooledItem(powerDownPrefabs[0], powerDownSpawnPool, "PowerDown");
    }

    private PooledGameObject CreatePooledPowerUp()
    {
        return CreatePooledItem(powerUpPrefabs[0], powerUpSpawnPool, "PowerUp");
    }

    private PooledGameObject CreatePooledItem(PooledGameObject prefab, IObjectPool<PooledGameObject> spawnPool, string spawnedName)
    {
        PooledGameObject spawned = Instantiate(prefab, spawnedParent);
        spawned.SetPool(spawnPool);
        spawned.name = spawnedName;
        return spawned;
    }

    private void OnTakeFromPoolPowerDown(PooledGameObject obj)
    {
        Vector3 scale = new(GameRandom.Core.NextFloat(1f, 3f), 1f, 1f);
        OnTakeFromPool(obj, powerDownPrefabs, scale);
    }

    private void OnTakeFromPoolPowerUp(PooledGameObject obj)
    {
        OnTakeFromPool(obj, powerUpPrefabs, Vector3.one);
    }

    private void OnTakeFromPool(PooledGameObject pooledObject, PooledGameObject[] prefabs, Vector3 scale)
    {
        PooledGameObject randomPrefab = GameRandom.Core.NextElement(prefabs);
        Vector3 randomPosition = new(GameRandom.Core.NextFloat(-8f, 8f), transform.position.y, transform.position.z);
        pooledObject.transform.SetPositionAndRotation(randomPosition, transform.rotation);
        pooledObject.transform.localScale = scale;

        SpriteRenderer pooledSpriteRenderer = pooledObject.GetComponent<SpriteRenderer>();
        SpriteRenderer prefabSpriteRenderer = randomPrefab.GetComponent<SpriteRenderer>();
        pooledSpriteRenderer.sprite = prefabSpriteRenderer.sprite;
        pooledSpriteRenderer.color = prefabSpriteRenderer.color;

        pooledObject.GetComponent<AudioSource>().clip = randomPrefab.GetComponent<AudioSource>().clip;

        PolygonCollider2D pooledCollider = pooledObject.GetComponent<PolygonCollider2D>();
        PolygonCollider2D prefabCollider = randomPrefab.GetComponent<PolygonCollider2D>();
        pooledCollider.isTrigger = prefabCollider.isTrigger;    
        pooledCollider.pathCount = prefabCollider.pathCount;
        for (int i = 0; i < prefabCollider.pathCount; i++)
        {
            pooledCollider.SetPath(i, prefabCollider.GetPath(i));
        }

        pooledObject.gameObject.SetActive(true);
    }

    private void OnReturnedToPool(PooledGameObject obj)
    {
        obj.gameObject.SetActive(false);
    }

    private void OnDestroyPoolObject(PooledGameObject obj)
    {
        Destroy(obj);
    }
}

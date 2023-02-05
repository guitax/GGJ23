using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class RootSpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameConfig gameConfig;
    [SerializeField]
    private int ClonesPerSecond = 10;
    [SerializeField]
    private Transform spawnedParent;
    [SerializeField]
    private PooledGameObject spawnPrefab;
    private SpriteRenderer spawnPrefabSpriteRenderer;

    private IObjectPool<PooledGameObject> rootSpawnPool;

    private void Awake()
    {
        rootSpawnPool = new ObjectPool<PooledGameObject>(
            CreatePooledItem,
            OnTakeFromPool,
            OnReturnedToPool,
            OnDestroyPoolObject);

        spawnPrefabSpriteRenderer = spawnPrefab.GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        StartCoroutine(Trail());
    }

    private IEnumerator Trail()
    {
        for (; ; ) //while(true)
        {
            rootSpawnPool.Get();

            yield return new WaitForSeconds(1f / ClonesPerSecond);
        }
    }

    private PooledGameObject CreatePooledItem()
    {
        PooledGameObject spawned = Instantiate(spawnPrefab, spawnedParent);
        spawned.SetPool(rootSpawnPool);
        return spawned;
    }

    private void OnTakeFromPool(PooledGameObject obj)
    {
        obj.transform.SetPositionAndRotation(transform.position, transform.rotation);
        obj.transform.localScale = Vector3.one;
        SpriteRenderer spawnedSpriteRenderer = obj.GetComponent<SpriteRenderer>();
        spawnedSpriteRenderer.sprite = spawnPrefabSpriteRenderer.sprite;
        spawnedSpriteRenderer.color = spawnPrefabSpriteRenderer.color;
        obj.gameObject.SetActive(true);
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

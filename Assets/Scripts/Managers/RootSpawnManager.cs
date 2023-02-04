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
    private Transform parent;
    [SerializeField]
    private PooledGameObject spawnPrefab;

    private IObjectPool<PooledGameObject> rootSpawnPool;

    private void Awake()
    {
        rootSpawnPool = new ObjectPool<PooledGameObject>(
            CreatePooledItem,
            OnTakeFromPool,
            OnReturnedToPool,
            OnDestroyPoolObject);
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
        PooledGameObject spawned = Instantiate(spawnPrefab, parent);
        spawned.SetPool(rootSpawnPool);
        return spawned;
    }

    private void OnTakeFromPool(PooledGameObject obj)
    {
        obj.transform.SetPositionAndRotation(parent.position, parent.rotation);
        obj.transform.localScale = transform.localScale;
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

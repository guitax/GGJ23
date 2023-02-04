using UnityEngine;
using UnityEngine.Pool;

public class PooledGameObject : MonoBehaviour
{
    private IObjectPool<PooledGameObject> rootSpawnPool;

    public void SetPool(IObjectPool<PooledGameObject> spawnPool)
    {
        rootSpawnPool = spawnPool;
    }

    private void OnBecameInvisible()
    {
        rootSpawnPool.Release(this);
    }
}

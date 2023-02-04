using System.Collections;
using UnityEngine;

public class RootSpawnManager : MonoBehaviour
{
    public GameConfig gameConfig;
    public int ClonesPerSecond = 10;
    [SerializeField]
    private GameObject clone;
    [SerializeField]
    private Transform parent;

    void Start()
    {
        StartCoroutine(Trail());
    }

    IEnumerator Trail()
    {
        for (;;) //while(true)
        {
            var spawnedClone = Instantiate(clone, parent);
            spawnedClone.transform.position = transform.position;
            spawnedClone.transform.rotation = transform.rotation;
            spawnedClone.transform.localScale = transform.localScale;
            // SpriteRenderer cloneRenderer = clone.AddComponent<SpriteRenderer>();
            // cloneRenderer.sortingOrder = this.clone.sortingOrder - 1;

            yield return new WaitForSeconds(1f / ClonesPerSecond);
        }
    }
}
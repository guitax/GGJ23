using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MyTrailRenderer : MonoBehaviour
{
    public GameConfig gameConfig;
    public int ClonesPerSecond = 10;
    [SerializeField]
    private GameObject clone;

    void Start()
    {
        StartCoroutine(Trail());
    }

    IEnumerator Trail()
    {
        for (;;) //while(true)
        {
            var spawnedClone = Instantiate(clone);
            spawnedClone.transform.position = transform.position;
            spawnedClone.transform.localScale = transform.localScale;
            // SpriteRenderer cloneRenderer = clone.AddComponent<SpriteRenderer>();
            // cloneRenderer.sortingOrder = this.clone.sortingOrder - 1;

            yield return new WaitForSeconds(1f / ClonesPerSecond);
        }
    }
}
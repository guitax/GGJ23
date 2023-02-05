using System.Collections;
using UnityEngine;

public class RootGrowthManager : MonoBehaviour
{
    [SerializeField]
    private float waitingTime = 3f;
    [SerializeField]
    private float probability = 0.1f;
    [SerializeField]
    private Sprite[] grownRootSprites;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        StartCoroutine(ChangeSpriteAfterWaiting());
    }

    private IEnumerator ChangeSpriteAfterWaiting()
    {
        yield return new WaitForSeconds(waitingTime);
        StartCoroutine(ChangeSprite());
    }

    private IEnumerator ChangeSprite()
    {
        if (GameRandom.Core.NextFloat(1f) < probability)
        {
            spriteRenderer.sprite = GameRandom.Core.NextElement(grownRootSprites);
        }
        else
        {
            spriteRenderer.color = Color.clear;
        }

        yield return null;
    }
}

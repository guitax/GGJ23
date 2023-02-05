using UnityEngine;

public class RootGrowthManager : MonoBehaviour
{
    [SerializeField]
    private float growthProbability = 1f;
    [SerializeField]
    private float shrinkProbability = 0f;
    [SerializeField]
    private Sprite[] grownRootSprites;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        ChangeSprite();
    }

    private void ChangeSprite()
    {
        float random = GameRandom.Core.NextFloat(1f);
        if (random < growthProbability)
        {
            spriteRenderer.sprite = GameRandom.Core.NextElement(grownRootSprites);
            transform.localScale = new Vector3(GameRandom.Core.NextFloat(0.8f, 1f), 1f, 1f);
        }
        else if (random < shrinkProbability)
        {
            spriteRenderer.color = Color.clear;
        }
    }
}

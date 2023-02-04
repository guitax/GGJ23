using UnityEngine;

public class ScrollingManager : MonoBehaviour
{
    [SerializeField]
    private GameConfig gameConfig;

    // Update is called once per frame
    private void Update()
    {
        transform.position = new Vector2(0f, transform.position.y + (gameConfig.speed * Time.deltaTime));
    }
}

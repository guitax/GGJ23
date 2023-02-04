using UnityEngine;

public class ScrollingManager : MonoBehaviour
{
    [SerializeField]
    private GameConfig gameConfig;

    // Update is called once per frame
    private void Update()
    {
        transform.position = new Vector3(0f, transform.position.y + (gameConfig.speed * Time.deltaTime), transform.position.z);
    }
}

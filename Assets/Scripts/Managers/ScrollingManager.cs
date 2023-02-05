using UnityEngine;

public class ScrollingManager : MonoBehaviour
{
    private void Update()
    {
        transform.position = new Vector3(0f, transform.position.y + (MainGameManager.Instance.gameConfig.speed * Time.deltaTime), transform.position.z);
    }
}

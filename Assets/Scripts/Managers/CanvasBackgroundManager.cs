using UnityEngine;
using UnityEngine.UI;

public class CanvasBackgroundManager : MonoBehaviour
{
    private RawImage rawImage;

    private void Awake()
    {
        rawImage = GetComponent<RawImage>();
    }

    private void Update()
    {
        rawImage.uvRect = new Rect(rawImage.uvRect.position + (MainGameManager.Instance.gameConfig.backgroundSpeed * Time.deltaTime * Vector2.down), rawImage.uvRect.size);
    }
}

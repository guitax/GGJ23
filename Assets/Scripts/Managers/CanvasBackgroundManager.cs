using UnityEngine;
using UnityEngine.UI;

public class CanvasBackgroundManager : MonoBehaviour
{
    private RawImage rawImage;
    [SerializeField]
    private GameConfig gameConfig;
   
    private void Awake()
    {
        rawImage = GetComponent<RawImage>();
    }

    private void Update()
    {
        rawImage.uvRect = new Rect(rawImage.uvRect.position + Vector2.down * gameConfig.backgroundSpeed * Time.deltaTime, rawImage.uvRect.size);
    }
}

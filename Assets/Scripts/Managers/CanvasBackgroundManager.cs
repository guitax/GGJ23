using UnityEngine;
using UnityEngine.UI;

public class BackgroundManager : MonoBehaviour
{
    private RawImage rawImage;
    [SerializeField]
    private float speed = 0.05f;

    private void Awake()
    {
        rawImage = GetComponent<RawImage>();
    }

    private void Update()
    {
        rawImage.uvRect = new Rect(rawImage.uvRect.position + Vector2.up * speed * Time.deltaTime, rawImage.uvRect.size);
    }
}

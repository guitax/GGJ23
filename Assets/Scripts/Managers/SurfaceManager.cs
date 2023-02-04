using UnityEngine;

public class SurfaceManager : MonoBehaviour
{
    [SerializeField]
    private GameConfig gameConfig;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    
    // Update is called once per frame
    void Update()
    {
        var currentPosition = transform.position;
        var transformedVector = currentPosition + gameConfig.speed * Time.deltaTime * transform.up;

        var screenVector = mainCamera.WorldToViewportPoint(transformedVector);

        if (screenVector.y  > 0.75f)
            return;
        
        transform.position = new Vector2(currentPosition.x, transformedVector.y);
    }
}

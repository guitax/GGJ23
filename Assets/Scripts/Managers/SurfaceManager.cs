using UnityEngine;

public class SurfaceManager : MonoBehaviour
{
    [SerializeField]
    private GameConfig gameConfig;

    [SerializeField]
    private float upperBound = 1.0f;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    
    // Update is called once per frame
    void Update()
    {
        var currentPosition = transform.position;
        var transformedVector = currentPosition + gameConfig.surfaceSpeed * Time.deltaTime * transform.up;

        var screenVector = mainCamera.WorldToViewportPoint(transformedVector);

        if (screenVector.y  > upperBound)
            return;
            
        transform.position = new Vector2(currentPosition.x, transformedVector.y);
    }
}

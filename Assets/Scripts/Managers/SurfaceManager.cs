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
    private void Update()
    {
        Vector3 transformedVector = transform.position + (gameConfig.surfaceSpeed * Time.deltaTime * transform.up);

        Vector3 screenVector = mainCamera.WorldToViewportPoint(transformedVector);

        if (screenVector.y > upperBound)
        {
            return;
        }

        transform.position = new Vector3(transform.position.x, transformedVector.y, transform.position.z);
    }
}

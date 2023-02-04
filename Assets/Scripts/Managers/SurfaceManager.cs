using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SurfaceManager : MonoBehaviour
{
    [SerializeField]
    private GameConfig gameConfig;

    [SerializeField]
    private float upperBound = 1.0f;

    private Camera mainCamera;

    private List<GameObject> greens;

    private void Start()
    {
        mainCamera = Camera.main;
        greens = createGreens();
    }
    
    public void PowerUpEvent(float aNumber)
    {
        var firstActive = greens.First(g => g.activeSelf);

        firstActive.SetActive(false);
    }

    public void PowerDownEvent(float aNumber)
    {
        var firstActive = greens.First(g => !g.activeSelf);

        firstActive.SetActive(true);
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
    
    private List<GameObject> createGreens()
    {
        
        var green1 = GameObject.Find("green1");
        var green2 = GameObject.Find("green2");
        var green3 = GameObject.Find("green3");
        var green4 = GameObject.Find("green4");

        return new List<GameObject>
        {
            green1, green2, green3, green4
        };
    }
}

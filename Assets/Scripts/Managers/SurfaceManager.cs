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

    void Start()
    {
        mainCamera = Camera.main;
        greens = createGreens();
        
        PlayerManager.SurfacePowerDown += PowerDownEvent;
        PlayerManager.SurfacePowerUp += PowerUpEvent;
    }

    private void PowerUpEvent()
    {
        var firstActive = greens.FirstOrDefault(g => !g.activeSelf);
        
        if(firstActive == null)
            return;
        
        firstActive.SetActive(true);
    }

    private void PowerDownEvent()
    {
        var activeGreens = greens.Where(g => g.activeSelf).ToList();
        if (activeGreens.Count() == 1)
        {
            PlayerManager.SurfaceDeath.Invoke();
        }
        
        var firstActive = activeGreens.First();
        
        firstActive.SetActive(false);
        
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

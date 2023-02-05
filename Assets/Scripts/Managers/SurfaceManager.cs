using System.Linq;
using UnityEngine;

public class SurfaceManager : MonoBehaviour
{
    [SerializeField]
    private float upperBound = 1.0f;
    [SerializeField]
    private GameObject[] greens;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        
        Debug.Log("Start surface manager");

        PlayerManager.SurfacePowerDown += PowerDownEvent;
        PlayerManager.SurfacePowerUp += PowerUpEvent;
    }

    private void PowerUpEvent()
    {
        GameObject firstInactive = greens.FirstOrDefault(g => !g.activeSelf);
        if (firstInactive != null)
        {
            firstInactive.SetActive(true);
        }
    }

    private void PowerDownEvent()
    {
        GameObject firstActive = greens.FirstOrDefault(g => g.activeSelf);
        if (firstActive != null)
        {
            firstActive.SetActive(false);
        }

        if (!greens.Any(g => g.activeSelf))
        {
            PlayerManager.SurfaceDeath.Invoke();
        }
    }

    private void Update()
    {
        Vector3 transformedVector = transform.position + (MainGameManager.Instance.gameConfig.surfaceSpeed * Time.deltaTime * transform.up);

        Vector3 screenVector = mainCamera.WorldToViewportPoint(transformedVector);

        if (screenVector.y > upperBound)
        {
            return;
        }

        transform.position = new Vector3(transform.position.x, transformedVector.y, transform.position.z);
    }

    private void OnDestroy()
    {
        PlayerManager.SurfacePowerDown -= PowerDownEvent;
        PlayerManager.SurfacePowerUp -= PowerUpEvent;
    }
}

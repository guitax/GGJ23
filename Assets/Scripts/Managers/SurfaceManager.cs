using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SurfaceManager : MonoBehaviour
{
    [SerializeField]
    private float upperBound = 1.0f;

    private Camera mainCamera;
    private GameObject[] greens;

    private void Start()
    {
        mainCamera = Camera.main;

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
        GameObject firstActive = greens.FirstOrDefault();
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
}

using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private const float ForwardDirectionDegrees = 0f;

    [SerializeField]
    private float rotationSpeed = 35f;
    [SerializeField]
    [Range(45f, 75f)]
    private float maxRotationDegrees = 60f;

    private PlayerInputActions playerInputActions;
    private Camera mainCamera;
    
    public delegate void HealthEvent();

    public static HealthEvent SurfacePowerUp;
    public static HealthEvent SurfacePowerDown;
    public static HealthEvent SurfaceDeath;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        Move();
    }

    private void OnDestroy()
    {
        playerInputActions.Player.Disable();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        string name1 = collider.gameObject.name;
        string name2 = gameObject.name;

        Debug.Log("Collision between " + name1 + " and " + name2);
        
        switch (collider.name)
        {
            case "PowerUp":
                SurfacePowerUp?.Invoke();
                break;
            case "PowerDown":
                SurfacePowerDown?.Invoke();
                break;
            default:
                SurfaceDeath?.Invoke();
                break;
        }
        
    }

    private void Move()
    {
        float moveDirection = playerInputActions.Player.Move.ReadValue<float>();

        if (moveDirection != 0f)
        {
            float targetAngle = GetTargetAngle(moveDirection);
            transform.rotation = Quaternion.Euler(0f, 0f, targetAngle);
        }

        UpdateXPosition();
    }

    private void UpdateXPosition()
    {
        Vector3 transformedVector = transform.position - (MainGameManager.Instance.gameConfig.speed * Time.deltaTime * transform.up);
        Vector3 screenVector = mainCamera.WorldToViewportPoint(transformedVector);

        if (screenVector.x - MainGameManager.Instance.gameConfig.boundaryOffSet < 0f || screenVector.x + MainGameManager.Instance.gameConfig.boundaryOffSet > 1f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(Vector3.down), Time.deltaTime);
            return;
        }

        transform.position = new Vector3(transformedVector.x, transform.position.y, transform.position.z);
    }

    private float GetTargetAngle(float moveDirection)
    {
        float targetAngle = transform.rotation.eulerAngles.z;
        if (targetAngle > 180f)
        {
            targetAngle -= 360f;
        }

        if (moveDirection < 0f && targetAngle > ForwardDirectionDegrees - maxRotationDegrees)
        {
            targetAngle -= rotationSpeed * Time.deltaTime;
        }
        else if (moveDirection > 0f && targetAngle < ForwardDirectionDegrees + maxRotationDegrees)
        {
            targetAngle += rotationSpeed * Time.deltaTime;
        }

        return targetAngle;
    }
}

using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private const float ForwardDirectionDegrees = 0f;
   

    [SerializeField]
    private GameConfig gameConfig;
    [SerializeField]
    private float rotationSpeed = 25f;
    [SerializeField]
    [Range(45f, 75f)]
    private float maxRotationDegrees = 60f;
    private const float BoundaryOffSet = 0.01f;

    private PlayerInputActions playerInputActions;
    private Camera mainCamera;


    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }

    private void Update()
    {
        Move();
    }

    private void OnDestroy()
    {
        playerInputActions.Player.Disable();
    }

    private void Move()
    {
        float moveDirection = playerInputActions.Player.Move.ReadValue<float>();
        Debug.Log($"Move: {moveDirection}");

        if (moveDirection != 0f)
        {
            float targetAngle = GetTargetAngle(moveDirection);
            transform.rotation = Quaternion.Euler(0, 0, targetAngle);
        }

        UpdateXPosition();
    }

    private void UpdateXPosition()
    {
        var currentPosition = transform.position;
        var transformedVector = currentPosition - gameConfig.speed * Time.deltaTime * transform.up;
        var screenVector = mainCamera.WorldToViewportPoint(transformedVector);

        if (screenVector.x - BoundaryOffSet < 0f || screenVector.x + BoundaryOffSet > 1f )
            return;

        transform.position = new Vector2(transformedVector.x, currentPosition.y);
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

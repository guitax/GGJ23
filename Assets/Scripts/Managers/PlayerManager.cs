using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private const float ForwardDirectionDegrees = 0f;

    [SerializeField]
    private float speed = 2f;
    [SerializeField]
    private float rotationSpeed = 25f;
    [SerializeField]
    [Range(45f, 90f)]
    private float maxRotationDegrees = 60f;

    private PlayerInputActions playerInputActions;

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

        transform.position -= transform.up * speed * Time.deltaTime;
    }

    private float GetTargetAngle(float moveDirection)
    {
        float targetAngle = transform.rotation.eulerAngles.z;

        targetAngle = targetAngle > 180f ? targetAngle - 360f : targetAngle;

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

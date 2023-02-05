using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    private const float ForwardDirectionDegrees = 0f;

    [SerializeField]
    private RectTransform boundary;
    [SerializeField]
    private float rotationSpeed = 35f;
    [SerializeField]
    [Range(45f, 75f)]
    private float maxRotationDegrees = 60f;
    [SerializeField]
    private bool godMode;
    private GameScore gameScore;

    private Camera mainCamera;

    public PlayerInputActions playerInputActions;

    public delegate void HealthEvent();

    public static HealthEvent SurfacePowerUp;
    public static HealthEvent SurfacePowerDown;
    public static HealthEvent SurfaceDeath;
    private bool isDead;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        gameScore = new GameScore();

        SurfaceDeath += OnDeath;
    }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (isDead)
        {
            return;
        }

        if (MainGameManager.Instance.IsPlaying)
        {
            gameScore.LifeTime += Time.deltaTime;
        }

        Move();
    }

    private void OnDestroy()
    {
        playerInputActions.Player.Disable();
        SurfaceDeath -= OnDeath;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        string name1 = collider.gameObject.name;
        string name2 = gameObject.name;

        //Debug.Log("Collision between " + name1 + " and " + name2);

        switch (collider.name)
        {
            case "PowerDown":
                gameScore.TotalPowerDowns++;
                SurfacePowerDown?.Invoke();
                break;
            case "PowerUp":
                gameScore.TotalPowerUps++;
                SurfacePowerUp?.Invoke();
                break;
            default:
                break;
        }
    }

    private void OnDeath()
    {
        if (!godMode)
        {
            isDead = true;
            MainGameManager.Instance.StopPlay();
            MainGameManager.Instance.GameScore = gameScore;
            SceneManager.LoadScene("DeathScene");
        }
    }

    private void Move()
    {
        float moveDirection = playerInputActions.Player.Move.ReadValue<float>();

        if (moveDirection != 0f)
        {
            if (!MainGameManager.Instance.IsPlaying)
            {
                MainGameManager.Instance.StartPlay();
            }

            float targetAngle = GetTargetAngle(moveDirection);
            transform.rotation = Quaternion.Euler(0f, 0f, targetAngle);
        }

        if (MainGameManager.Instance.IsPlaying)
        {
            UpdateXPosition();
        }
    }

    private void UpdateXPosition()
    {
        Vector3 transformedVector = transform.position - (MainGameManager.Instance.gameConfig.speed * Time.deltaTime * transform.up);

        if (transformedVector.x < -MainGameManager.Instance.gameConfig.boundaryOffset
            || transformedVector.x > MainGameManager.Instance.gameConfig.boundaryOffset)
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

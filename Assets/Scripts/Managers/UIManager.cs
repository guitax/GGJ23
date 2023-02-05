using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;

    private void Start()
    {
        GameScore gameScore = MainGameManager.Instance?.GameScore ?? default;
        _scoreText.text = $"You survived {(int)gameScore.LifeTime} years\r\n\r\nTotal power ups: {gameScore.TotalPowerUps}\r\n\r\nTotal power down: {gameScore.TotalPowerDowns}";
    }
}

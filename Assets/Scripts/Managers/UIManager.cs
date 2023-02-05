using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    
    // Start is called before the first frame update
    void Start()
    {
        GameScore gameScore = MainGameManager.Instance.GameScore;
        _scoreText.text = $"You survived {(int)gameScore.LifeTime} years\r\nTotal power ups: {gameScore.TotalPowerUps}\r\nTotal power down: {gameScore.TotalPowerDowns}";
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}

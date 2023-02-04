using UnityEngine;

[CreateAssetMenu(menuName = "SO Configs/" + nameof(GameConfig), fileName = nameof(GameConfig))]
public class GameConfig : ScriptableObject
{
    public float speed;
    public float boundaryOffSet;
}

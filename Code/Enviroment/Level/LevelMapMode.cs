using UnityEngine;

public class LevelMapMode : MonoBehaviour
{
    [SerializeField] private LevelTypeMode _mode;

    public LevelTypeMode Mode => _mode;
}

using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private LevelTypeMode _mode;

    public LevelTypeMode Mode => _mode;

    public bool IsBusy { get; private set; }

    public void ToBusy() => IsBusy = true; 
}

using UnityEngine;

public class EnemyTransition : MonoBehaviour
{
    [SerializeField] private EnemyState _targetState;

    public EnemyState TargetState => _targetState;

    public bool NeedTransit { get; protected set; }

    private void OnEnable() => NeedTransit = false;
}
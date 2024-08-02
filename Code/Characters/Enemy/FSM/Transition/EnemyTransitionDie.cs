using UnityEngine;

[RequireComponent(typeof(Stats))]
public class EnemyTransitionDie : EnemyTransition
{
    private Stats _stats;

    private void Awake()
    {
        _stats = GetComponent<Stats>();
    }

    private void Update()
    {
        if (_stats.IsDead())
            NeedTransit = true;
    }
}

using UnityEngine;

[RequireComponent(typeof(MeleeAttack))]
[RequireComponent(typeof(Stats))]
[RequireComponent(typeof(CharacterAnimation))]
public class EmemyAttackState : EnemyState
{
    private MeleeAttack _meleeAttack;
    private Stats _stats;
    private CharacterAnimation _animation;

    private void Awake()
    {
        _meleeAttack = GetComponent<MeleeAttack>();
        _stats = GetComponent<Stats>();
        _animation = GetComponent<CharacterAnimation>();
    }

    private void Update()
    {
        if (_meleeAttack.TryHit(_stats.Damage))
        {
            _animation.ToAttack();
        }
    }
}

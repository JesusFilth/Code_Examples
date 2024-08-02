
using Reflex.Attributes;

public class EnemyStats : Stats
{
    [Inject] private ILevelEnemySettings _enemySettings;

    private int _defaultLife = 1;

    private void Start()
    {
        Initialize();
        ResetToDefault();
    }

    public void ResetToDefault()
    {
        CurrentHealth = MaxHealth;
        SetLife(_defaultLife);

        UpadateViewChanges();
    }

    private void Initialize()
    {
        _defaultLife = Life;
        SetDamage(Damage+_enemySettings.GetImproveStats());
        SetMaxHealth(MaxHealth+_enemySettings.GetImproveStats());
    }
}

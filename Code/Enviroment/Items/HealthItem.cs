using UnityEngine;

public class HealthItem : Item
{
    [Space][SerializeField] private float _health;

    protected override void Use(Player player)
    {
        if (player.TryGetComponent(out Stats stats))
        {
            stats.AddHealth(_health);
        }
    }
}

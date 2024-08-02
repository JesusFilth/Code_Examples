using UnityEngine;

public class LifeItem : Item
{
    [Space][SerializeField] private int _count;

    protected override void Use(Player player)
    {
        if(player.TryGetComponent(out Stats stats))
        {
            stats.AddLife(_count);
        }
    }
}

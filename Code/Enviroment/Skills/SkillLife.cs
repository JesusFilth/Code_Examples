public class SkillLife : SkillStratigy
{
    private const int MaxValue = 1;

    public override void AddProperty(PlayerStats stat)
    {
        stat.AddLife(MaxValue);
    }

    public override void SetValue(float value)
    {
        base.SetValue(MaxValue);
    }
}

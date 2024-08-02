public class SkillHealth : SkillStratigy
{
    public override void AddProperty(PlayerStats stat)
    {
        stat.AddHealth(Value);
    }
}

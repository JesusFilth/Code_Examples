public class SkillDamage : SkillStratigy
{
    public override void AddProperty(PlayerStats stat)
    {
        stat.AddDamage(Value);
    }
}
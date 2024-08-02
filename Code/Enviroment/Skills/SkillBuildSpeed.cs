public class SkillBuildSpeed : SkillStratigy
{
    public override void AddProperty(PlayerStats stat)
    {
        stat.AddBuildPower(Value);
    }
}

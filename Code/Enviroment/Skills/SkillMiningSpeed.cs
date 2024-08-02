public class SkillMiningSpeed : SkillStratigy
{
    public override void AddProperty(PlayerStats stat)
    {
        stat.AddMiningPower(Value);
    }
}
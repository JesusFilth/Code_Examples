public class SkillMineralConteinerSize : SkillStratigy
{
    public override void AddProperty(PlayerStats stat)
    {
        stat.AddMaxMineralConteinerSize((int)Value);
    }
}
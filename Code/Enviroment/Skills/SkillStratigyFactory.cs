public class SkillStratigyFactory
{
    public SkillStratigy GetSkill(SkillStatType statType)
    {
        switch (statType)
        {
            case SkillStatType.Health: return CreateHealth();
            case SkillStatType.Life: return CreateLife();
            case SkillStatType.Damage: return CreateDamage();
            case SkillStatType.BuildSpeed: return CreateBuildSpeed();
            case SkillStatType.MiningSpeed: return CreateMiningSpeed();
            case SkillStatType.TruckSize: return CreateMineralConteinerSize();
            case SkillStatType.Gold: return CreateGold();
            default: return null;
        }
    }

    public SkillStratigy CreateHealth() => new SkillHealth();

    public SkillStratigy CreateLife() => new SkillLife();

    public SkillStratigy CreateDamage() => new SkillDamage();

    public SkillStratigy CreateBuildSpeed() => new SkillBuildSpeed();

    public SkillStratigy CreateMiningSpeed() => new SkillMiningSpeed();

    public SkillStratigy CreateMineralConteinerSize() => new SkillMineralConteinerSize();

    public SkillStratigy CreateGold() => new SkillGold();
}

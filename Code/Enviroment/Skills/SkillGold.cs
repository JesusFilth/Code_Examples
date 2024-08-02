public class SkillGold : SkillStratigy
{
    private const int GoldUpValue = 10;

    public override void AddProperty(PlayerStats stat)
    {
        if(stat.gameObject.TryGetComponent(out IWallet wallet))
        {
            wallet.AddCoin((int)Value);
        }
    }

    public override void SetValue(float value)
    {
        base.SetValue(value * GoldUpValue);
    }
}

using Lean.Localization;

public class LocalizationTranslate
{
    private const string StatHealth = "Health";
    private const string StatLife = "Life";
    private const string StatDamage = "Damage";
    private const string StatBuildSpeed = "BuildSpeed";
    private const string StatMiningSpeed = "MiningSpeed";
    private const string StatTruckSize = "TruckSize";

    private const string NeedMoreStars = "NeedMoreStars";
    private const string LevelModeClose = "LevelModeClose";
    private const string NotEnoughGold = "NotEnoughGold";
    private const string SkillUp = "SkillUp";
    private const string Purchased = "Purchased";

    private const string DontOre = "DontOre";
    private const string ConteinerFull = "ConteinerFull";

    public string GetStatName(SkillStatType stat)
    {
        switch (stat)
        {
            case SkillStatType.Health: return LeanLocalization.GetTranslationText(StatHealth);
            case SkillStatType.Life: return LeanLocalization.GetTranslationText(StatLife);
            case SkillStatType.Damage: return LeanLocalization.GetTranslationText(StatDamage);
            case SkillStatType.BuildSpeed: return LeanLocalization.GetTranslationText(StatBuildSpeed);
            case SkillStatType.MiningSpeed: return LeanLocalization.GetTranslationText(StatMiningSpeed);
            case SkillStatType.TruckSize: return LeanLocalization.GetTranslationText(StatTruckSize);
            default: return null;
        }
    }

    public string GetMessage(LocalizationMessageType type)
    {
        switch (type)
        {
            case LocalizationMessageType.NeedMoreStars: return LeanLocalization.GetTranslationText(NeedMoreStars);
            case LocalizationMessageType.LevelModeClose: return LeanLocalization.GetTranslationText(LevelModeClose);
            case LocalizationMessageType.NotEnoughGold: return LeanLocalization.GetTranslationText(NotEnoughGold);
            case LocalizationMessageType.SkillUp: return LeanLocalization.GetTranslationText(SkillUp);
            case LocalizationMessageType.Purchased: return LeanLocalization.GetTranslationText(Purchased);

            case LocalizationMessageType.DontOre: return LeanLocalization.GetTranslationText(DontOre);
            case LocalizationMessageType.ConteinerFull: return LeanLocalization.GetTranslationText(ConteinerFull);

            default: return null;
        }
    }
}

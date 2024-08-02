using Reflex.Attributes;

public class SkillItem : Item
{
    [Inject] private GameUIStateMashine _gameUI;

    protected override void Use(Player player)
    {
        _gameUI.EnterIn<SkillsUIState>();
    }
}

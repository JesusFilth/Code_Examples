using System;

public class SkillsUIState : IGameUIState
{
    private IGameLevelView _view;

    public SkillsUIState(IGameLevelView gameMenuView)
    {
        if (gameMenuView == null)
            throw new ArgumentNullException(nameof(gameMenuView));

        _view = gameMenuView;
    }

    public void Close()
    {
        _view.Hide();
    }

    public void Open()
    {
        _view.Show();
    }
}

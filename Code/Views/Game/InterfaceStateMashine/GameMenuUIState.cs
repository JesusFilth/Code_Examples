using System;

public class GameMenuUIState : IGameUIState
{
    private IGameLevelView _view;

    public GameMenuUIState(IGameLevelView gameMenuView)
    {
        if(gameMenuView == null)
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

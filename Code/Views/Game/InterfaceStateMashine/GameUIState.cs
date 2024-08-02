using System;

public class GameUIState : IGameUIState
{
    private IGameLevelView _view;

    public GameUIState(IGameLevelView gameLevelView)
    {
        if (gameLevelView == null)
            throw new ArgumentNullException(nameof(gameLevelView));

        _view = gameLevelView;
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

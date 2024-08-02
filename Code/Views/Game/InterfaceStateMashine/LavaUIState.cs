using System;

public class LavaUIState : IGameUIState
{
    private IGameLevelView _view;

    public LavaUIState(IGameLevelView gameLevelView)
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

using IJunior.TypedScenes;

public class LoadMainMenuSceneState : IGameState
{
    public void Execute()
    {
        MainMenu.Load();
    }
}

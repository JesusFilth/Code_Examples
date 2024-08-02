public interface ILevelStorage
{
    LevelModel[] GetLevels();

    int GetAllStars();

    void AddStar(int indexLevel, LevelTypeMode mode);
}

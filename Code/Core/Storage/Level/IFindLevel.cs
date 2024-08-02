public interface IFindLevel
{
    bool TryGetLevel(int index, LevelTypeMode mode, out LevelMode level);
}

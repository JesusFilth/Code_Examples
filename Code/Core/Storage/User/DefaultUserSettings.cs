using UnityEngine;

[CreateAssetMenu(fileName = "DefaultUserSettings", menuName = "World of Cubes/DefaultUserSettings", order = 2)]

public class DefaultUserSettings : ScriptableObject
{
    [SerializeField] private string _name = "Player";
    [SerializeField] private int _gold;
    [SerializeField] private UserStatsModel _userStatsModel;

    public void Init(out UserModel user)
    {
        user = new UserModel(); 
        user.PlayerStats = new UserStatsModel();

        user.Name = _name;
        user.Gold = _gold;

        user.PlayerStats.Health = _userStatsModel.Health;
        user.PlayerStats.Damage = _userStatsModel.Damage;
        user.PlayerStats.CraftSpeed = _userStatsModel.CraftSpeed;
        user.PlayerStats.BuildSpeed = _userStatsModel.BuildSpeed;
        user.PlayerStats.MaxMineralConteiner = _userStatsModel.MaxMineralConteiner;
    }
}

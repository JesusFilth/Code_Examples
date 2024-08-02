using Reflex.Core;
using UnityEngine;

public class ReflexProjectInstaller : MonoBehaviour, IInstaller
{
    [SerializeField] private LevelStorage _gameLevelStorage;

    public void InstallBindings(ContainerBuilder containerBuilder)
    {
        containerBuilder.AddSingleton(_gameLevelStorage, 
            typeof(IFindLevel), 
            typeof(ILevelInfo), 
            typeof(IDefaultUser));

        containerBuilder.AddSingleton(new UserStorage(),
           typeof(IStateStorage),
           typeof(IGoldStorage),
           typeof(ILevelStorage),
           typeof(IUserStorage));

        containerBuilder.AddSingleton(new StateMashine());
    }
}

using Reflex.Core;
using Reflex.Extensions;
using Reflex.Injectors;
using UnityEngine;

public class GameLevelConteinerDI : MonoBehaviour
{
    public static GameLevelConteinerDI Instance { get; private set; }

    private Container _container;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Initialize();
        }
    }

    public void InitHot()
    {
        if (Instance == null)
        {
            Instance = this;
            Initialize();
        }
    }

    public void InjectRecursive(GameObject gameObject)
    {
        GameObjectInjector.InjectRecursive(gameObject, _container);
    }

    private void Initialize()
    {
        _container = gameObject.scene.GetSceneContainer();     
    }
}

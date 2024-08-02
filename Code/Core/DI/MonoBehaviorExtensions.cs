using Reflex.Extensions;
using Reflex.Injectors;
using UnityEngine;

public static class MonoBehaviorExtensions
{
    public static GameObject InstantiateInjected<T>(this MonoBehaviour monoBehaviour, GameObject original)
    {
        var conteiner = monoBehaviour.gameObject.scene.GetSceneContainer();
        var instiate = Object.Instantiate(original);
        GameObjectInjector.InjectRecursive(instiate, conteiner);

        return instiate;
    }
}

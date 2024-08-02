using Reflex.Core;
using UnityEngine;

public class MainMenuReflexInstaller : MonoBehaviour, IInstaller
{
    [SerializeField] private MessageBox _messageBox;

    public void InstallBindings(ContainerBuilder containerBuilder)
    {
        containerBuilder.AddSingleton(new LocalizationTranslate());
        containerBuilder.AddSingleton(_messageBox); 
    }
}

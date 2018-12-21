using Shared;
using UnityEngine;
using Zenject;

public class ProjectContextInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IUserInputManager>().To<UserInputManager>().AsSingle();
    }
}
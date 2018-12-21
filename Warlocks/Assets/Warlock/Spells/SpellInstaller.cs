using UnityEngine;
using Zenject;

public class SpellInstaller : MonoInstaller
{
    public SpellController spellController;

    public override void InstallBindings()
    {
        Container.Bind<SpellController>().FromInstance(spellController).AsSingle();
    }
}
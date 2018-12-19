using Services;
using UnityEngine;
using Zenject;

public class RbMovementInstaller : MonoInstaller
{
    public float velocity;
    public float amortizationTreshold;
    public float amortizationSpeedMult;
    public float posEqualityTreshold;

    public override void InstallBindings()
    {
        Container.Bind<IMovement>()
            .To<RbMovement>()
            .AsSingle()
            .WithArguments(velocity, amortizationTreshold, amortizationSpeedMult, posEqualityTreshold);
    }
}
using UnityEngine;
using Zenject;

namespace Krk.Elevators
{
    public class ElevatorInstaller : MonoInstaller<ElevatorInstaller>
    {
        [SerializeField] ElevatorConfig elevatorConfig;

        public override void InstallBindings()
        {
            Container.BindInstance(elevatorConfig).IfNotBound();
            Container.Bind<ElevatorController>().AsSingle();
        }
    }
}
using Krk.Elevators;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Krk.Doors
{
    public class DoorInstaller : MonoInstaller<DoorInstaller>
    {
        [FormerlySerializedAs("elevatorButtonConfig")] [SerializeField]
        FloorConfig floorConfig;

        public override void InstallBindings()
        {
            Container.BindInstance(floorConfig).IfNotBound();

            Container.Bind<DoorController>().AsSingle();
            Container.Bind<DoorAutoController>().AsSingle();
            Container.Bind<ElevatorDoorController>().AsSingle();
            Container.Bind<ElevatorButtonController>().AsSingle();
        }
    }
}
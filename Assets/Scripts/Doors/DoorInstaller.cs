using Zenject;

namespace Krk.Doors
{
    public class DoorInstaller : MonoInstaller<DoorInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<DoorController>().AsSingle();
        }
    }
}
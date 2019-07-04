using Zenject;

namespace Krk.Common.View
{
    public class ViewInstaller : MonoInstaller<ViewInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<DynamicContent>().AsSingle();
        }
    }
}
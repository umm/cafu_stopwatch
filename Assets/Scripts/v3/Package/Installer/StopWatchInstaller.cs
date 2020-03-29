using CAFU.StopWatch.Domain.Entity.Implement;
using CAFU.StopWatch.Domain.UseCase.Implement;
using CAFU.StopWatch.Domain.UseCase.Interface.Entity;
using CAFU.StopWatch.Domain.UseCase.Interface.UseCase;
using UnityEngine;
using Zenject;

namespace CAFU.StopWatch.Package.Installer
{
    public class StopWatchInstaller : MonoInstaller<StopWatchInstaller>
    {
        [SerializeField] private string injectId = default;

        private string InjectId => injectId;

        public override void InstallBindings()
        {
            (InjectId == default || InjectId.Length == 0
                    ? Container.Bind(typeof(IStopWatchUseCase), typeof(IStopWatchState))
                    : Container.Bind(typeof(IStopWatchUseCase), typeof(IStopWatchState)).WithId(InjectId))
                .FromSubContainerResolve()
                .ByInstaller<SubContainerInstaller>()
                .AsSingle()
                .NonLazy();
        }

        private class SubContainerInstaller : Installer<SubContainerInstaller>
        {
            public override void InstallBindings()
            {
                Container.BindInterfacesTo<StopWatchUseCase>().AsCached();

                Container.BindInterfacesTo<StopWatchState>().AsCached();
            }
        }
    }
}

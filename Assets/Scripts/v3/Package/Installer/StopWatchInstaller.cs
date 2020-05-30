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
            var contractTypes = new[]
            {
                typeof(IStopWatchUseCase),
                typeof(IStopWatchState),
            };

            (string.IsNullOrEmpty(InjectId)
                    ? Container.Bind(contractTypes)
                    : Container.Bind(contractTypes).WithId(InjectId))
                .FromSubContainerResolve()
                .ByMethod(InstallSubContainer)
                .WithKernel()
                .AsCached();
        }

        private void InstallSubContainer(DiContainer subContainer)
        {
            subContainer.BindInterfacesTo<StopWatchUseCase>().AsSingle();
            subContainer.BindInterfacesTo<StopWatchState>().AsSingle();
        }
    }
}

using CAFU.Core;
using CAFU.StopWatch.Package.Enum;
using UniRx;

namespace CAFU.StopWatch.Domain.UseCase.Interface.Entity
{
    public interface IStopWatchState : IEntity
    {
        IReadOnlyReactiveProperty<StopWatchStateType> CurrentState { get; }

        IReadOnlyReactiveProperty<float> ElapsedTime { get; }
    }
}

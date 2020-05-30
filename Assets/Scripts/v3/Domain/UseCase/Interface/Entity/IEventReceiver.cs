using CAFU.Core;
using CAFU.StopWatch.Package.Enum;
using UniRx;

namespace CAFU.StopWatch.Domain.UseCase.Interface.Entity
{
    internal interface IEventReceiver : IEntity
    {
        IReactiveProperty<StopWatchStateType> CurrentState { get; }

        IReactiveProperty<float> ElapsedTime { get; }
    }
}

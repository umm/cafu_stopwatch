using CAFU.StopWatch.Domain.UseCase.Interface.Entity;
using CAFU.StopWatch.Package.Enum;
using UniRx;

namespace CAFU.StopWatch.Domain.Entity.Implement
{
    public class StopWatchState :
        IEventReceiver,
        IStopWatchState
    {
        private IReactiveProperty<StopWatchStateType> CurrentState { get; } =
            new ReactiveProperty<StopWatchStateType>(StopWatchStateType.Stopped);

        IReactiveProperty<StopWatchStateType> IEventReceiver.CurrentState => CurrentState;

        IReadOnlyReactiveProperty<StopWatchStateType> IStopWatchState.CurrentState =>
            CurrentState.ToReadOnlyReactiveProperty();

        private IReactiveProperty<float> ElapsedTime { get; } = new ReactiveProperty<float>();

        IReactiveProperty<float> IEventReceiver.ElapsedTime => ElapsedTime;

        IReadOnlyReactiveProperty<float> IStopWatchState.ElapsedTime => ElapsedTime.ToReadOnlyReactiveProperty();
    }
}

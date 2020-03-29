using UniRx;

namespace CAFU.StopWatch.Package.Component
{
    public interface IStopWatchEventReceiver
    {
        IReactiveProperty<float> ElapsedTime { get; }

        void OnStarted();

        void OnStopped();

        void OnPaused();

        void OnResumed();
    }
}

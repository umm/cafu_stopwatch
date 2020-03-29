using System;
using CAFU.StopWatch.Domain.UseCase.Interface.Entity;
using CAFU.StopWatch.Domain.UseCase.Interface.UseCase;
using CAFU.StopWatch.Package.Enum;
using UniRx;
using UnityModule;
using Zenject;

namespace CAFU.StopWatch.Domain.UseCase.Implement
{
    internal class StopWatchUseCase :
        IStopWatchUseCase,
        IInitializable,
        IDisposable
    {
        private IStopWatch StopWatch { get; } = new UnityModule.StopWatch();

        private CompositeDisposable Disposable { get; } = new CompositeDisposable();

        private IObservable<float> ElapsedTimeAsObservable() => StopWatch.TimeAsObservable;

        private IEventReceiver EventReceiver { get; }

        public StopWatchUseCase(IEventReceiver eventReceiver)
        {
            EventReceiver = eventReceiver;

            StartObservingStopWatchTime();
        }

        void IInitializable.Initialize()
        {
        }

        void IDisposable.Dispose() => Disposable.Dispose();

        private void StartObservingStopWatchTime()
        {
            ElapsedTimeAsObservable()
                .Subscribe(UpdateElapsedTime)
                .AddTo(Disposable);
        }

        private void UpdateElapsedTime(float time)
        {
            EventReceiver.ElapsedTime.Value = time;
        }

        void IStopWatchUseCase.Start()
        {
            StopWatch.Start();
            EventReceiver.CurrentState.Value = StopWatchStateType.Started;
        }

        void IStopWatchUseCase.Stop()
        {
            StopWatch.Stop();
            EventReceiver.CurrentState.Value = StopWatchStateType.Stopped;
        }

        void IStopWatchUseCase.Pause()
        {
            StopWatch.Pause();
            EventReceiver.CurrentState.Value = StopWatchStateType.Paused;
        }

        void IStopWatchUseCase.Resume()
        {
            StopWatch.Resume();
            EventReceiver.CurrentState.Value = StopWatchStateType.Started;
        }
    }
}
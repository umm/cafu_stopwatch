using System;
using CAFU.StopWatch.Domain.UseCase.Interface.Entity;
using CAFU.StopWatch.Domain.UseCase.Interface.UseCase;
using CAFU.StopWatch.Package.Enum;
using UniRx;
using UnityEngine;
using UnityModule;

namespace CAFU.StopWatch.Domain.UseCase.Implement
{
    internal class StopWatchUseCase :
        IStopWatchUseCase,
        IDisposable
    {
        private IStopWatch StopWatch { get; set; } = new UnityModule.StopWatch();

        private CompositeDisposable Disposable { get; } = new CompositeDisposable();

        private IEventReceiver EventReceiver { get; }

        public StopWatchUseCase(IEventReceiver eventReceiver)
        {
            EventReceiver = eventReceiver;
            Initialize();
        }

        private void Initialize()
        {
            Disposable.Clear();

            StopWatch = new UnityModule.StopWatch();
            EventReceiver.ElapsedTime.Value = 0;
            StopWatch
                .TimeAsObservable
                .Subscribe(UpdateElapsedTime)
                .AddTo(Disposable);
        }

        void IDisposable.Dispose() => Disposable.Dispose();

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

        void IStopWatchUseCase.Reset()
        {
            Initialize();
        }
    }
}
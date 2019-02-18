using System;
using CAFU.Core.Domain.UseCase;
using UniRx;
using UnityModule;

namespace CAFU.Stopwatch.Domain.UseCase
{
    public class StopwatchUseCase : IStopwatchUseCase
    {
        public class Factory : DefaultUseCaseFactory<StopwatchUseCase>
        {
            protected override void Initialize(StopwatchUseCase instance)
            {
                base.Initialize(instance);
                instance.Initialize();
            }
        }

        public IObservable<float> TimeAsObservable => this.Stopwatch.TimeAsObservable;
        public IObservable<Unit> StartedAsObservable => this.StartedSubject;
        public IObservable<float> StoppedTimeAsObservable => this.StoppedTimeSubject;
        public IObservable<float> PausedTimeAsObservable => this.PausedTimeSubject;
        public IObservable<Unit> ResumedTimeAsObservable => this.ResumedTimeSubject;
        public bool IsPlaying => this.Stopwatch.IsPlaying;
        public IObservable<bool> IsPlayingAsObservable => this.Stopwatch.IsPlayingAsObservable;

        protected IStopWatch Stopwatch { get; set; }
        private ISubject<Unit> StartedSubject { get; set; }
        private ISubject<float> StoppedTimeSubject { get; set; }
        private ISubject<float> PausedTimeSubject { get; set; }
        private ISubject<Unit> ResumedTimeSubject { get; set; }

        protected virtual void Initialize()
        {
            this.Stopwatch = new StopWatch();
            this.StartedSubject = new Subject<Unit>();
            this.StoppedTimeSubject = new Subject<float>();
            this.PausedTimeSubject = new Subject<float>();
            this.ResumedTimeSubject = new Subject<Unit>();
        }

        public void Start()
        {
            this.Stopwatch.Stop();
            this.Stopwatch.Start();
            this.StartedSubject.OnNext(Unit.Default);
        }

        public void Stop()
        {
            this.StoppedTimeSubject.OnNext(this.Stopwatch.Time);
            this.Stopwatch.Stop();
        }

        public void Pause()
        {
            this.PausedTimeSubject.OnNext(this.Stopwatch.Time);
            this.Stopwatch.Pause();
        }

        public void Resume()
        {
            this.ResumedTimeSubject.OnNext(Unit.Default);
            this.Stopwatch.Resume();
        }
    }
}

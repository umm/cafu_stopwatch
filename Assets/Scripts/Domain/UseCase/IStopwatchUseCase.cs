using System;
using CAFU.Core.Domain.UseCase;
using UniRx;

namespace CAFU.Stopwatch.Domain.UseCase
{
    public interface IStopwatchUseCase : IUseCase
    {
        /// <summary>
        /// Start Stopwatch
        /// </summary>
        void Start();

        /// <summary>
        /// Stop Stopwatch
        /// </summary>
        void Stop();

        /// <summary>
        /// Resume Stopwatch
        /// </summary>
        void Pause();

        /// <summary>
        /// Resume Stopwatch
        /// </summary>
        void Resume();

        /// <summary>
        /// Get stream of time (seconds) as observable
        /// </summary>
        IObservable<float> TimeAsObservable { get; }

        /// <summary>
        /// Started timming as observable
        /// </summary>
        IObservable<Unit> StartedAsObservable { get; }

        /// <summary>
        /// Stopped timming as observable
        /// </summary>
        IObservable<float> StoppedTimeAsObservable { get; }

        /// <summary>
        /// Stopwatch is either playing or not
        /// </summary>
        bool IsPlaying { get; }

        /// <summary>
        /// Check if it's playing.
        /// </summary>
        IObservable<bool> IsPlayingAsObservable { get; }
    }
}
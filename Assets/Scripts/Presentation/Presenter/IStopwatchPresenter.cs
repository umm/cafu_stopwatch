using System;
using CAFU.Core.Presentation.Presenter;
using CAFU.Stopwatch.Domain.UseCase;

namespace CAFU.Stopwatch.Presentation.View
{
    public interface IStopwatchPresenter : IPresenter
    {
        IStopwatchUseCase StopwatchUseCase { get; }
    }

    public static class IStopwatchPresenterExtension
    {
        public static void StartStopwatch(this IStopwatchPresenter presenter)
        {
            presenter.StopwatchUseCase.Start();
        }

        public static void StopStopwatch(this IStopwatchPresenter presenter)
        {
            presenter.StopwatchUseCase.Stop();
        }

        public static void ResumeStopwatch(this IStopwatchPresenter presenter)
        {
            presenter.StopwatchUseCase.Resume();
        }

        public static void PauseStopwatch(this IStopwatchPresenter presenter)
        {
            presenter.StopwatchUseCase.Pause();
        }

        public static IObservable<float> GetTimeAsObservable(this IStopwatchPresenter presenter)
        {
            return presenter.StopwatchUseCase.TimeAsObservable;
        }
    }
}
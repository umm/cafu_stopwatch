using CAFU.Core.Presentation.Presenter;
using CAFU.Stopwatch.Domain.UseCase;
using CAFU.Stopwatch.Presentation.View;

namespace Example.CAFU.Stopwatch.Presentation.Presenter
{
    public class SamplePresenter : IStopwatchPresenter
    {
        public class Factory : DefaultPresenterFactory<SamplePresenter>
        {
            protected override void Initialize(SamplePresenter instance)
            {
                base.Initialize(instance);
                instance.StopwatchUseCase = new StopwatchUseCase.Factory().Create();
            }
        }

        public IStopwatchUseCase StopwatchUseCase { get; private set; }
    }
}

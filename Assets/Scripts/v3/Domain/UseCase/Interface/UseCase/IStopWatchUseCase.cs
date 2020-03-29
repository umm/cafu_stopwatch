using CAFU.Core;

namespace CAFU.StopWatch.Domain.UseCase.Interface.UseCase
{
    public interface IStopWatchUseCase : IUseCase
    {
        void Start();

        void Stop();

        void Pause();

        void Resume();
    }
}

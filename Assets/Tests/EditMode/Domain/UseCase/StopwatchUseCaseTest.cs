using ExtraUniRx;
using NUnit.Framework;
using UnityModule;
using UniRx;

namespace CAFU.Stopwatch.Domain.UseCase
{
    public class StopwatchUseCaseTest
    {
        class StopwatchUseCaseSpy : StopwatchUseCase
        {
            public void Initialize(ISubject<float> frameDiffTime)
            {
                base.Initialize();
                this.Stopwatch = new StopWatch(frameDiffTime);
            }
        }

        private StopwatchUseCaseSpy usecase;
        private ISubject<float> frameDiffTime;

        [SetUp]
        public void Setup()
        {
            this.frameDiffTime = new Subject<float>();
            this.usecase = new StopwatchUseCaseSpy();
            this.usecase.Initialize(this.frameDiffTime);
        }

        [Test]
        public void StartedAsObservableTest()
        {
            Assert.IsFalse(this.usecase.IsPlaying);

            var observer = new TestObserver<Unit>();
            this.usecase.StartedAsObservable.Subscribe(observer);
            Assert.AreEqual(0, observer.OnNextCount);

            this.usecase.Start();
            Assert.AreEqual(1, observer.OnNextCount);

            this.usecase.Stop();
            Assert.AreEqual(1, observer.OnNextCount);

            this.usecase.Start();
            Assert.AreEqual(2, observer.OnNextCount);
        }
    }
}
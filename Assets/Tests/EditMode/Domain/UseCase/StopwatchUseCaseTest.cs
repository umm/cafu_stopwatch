using CAFU.Stopwatch.Domain.UseCase;
using ExtraUniRx;
using NUnit.Framework;
using UnityModule;
using UniRx;

namespace EditTest.CAFU.Stopwatch.Domain.UseCase
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

        [Test]
        public void StoppedAsObservableTest()
        {
            var observer = new TestObserver<float>();
            this.usecase.StoppedTimeAsObservable.Subscribe(observer);
            Assert.AreEqual(0, observer.OnNextCount);

            this.usecase.Start();
            Assert.AreEqual(0, observer.OnNextCount);

            this.frameDiffTime.OnNext(1f);
            Assert.AreEqual(0, observer.OnNextCount);

            this.usecase.Stop();
            Assert.AreEqual(1, observer.OnNextCount);
            Assert.AreEqual(1f, observer.OnNextValues[0]);
        }

        [Test]
        public void PausedAsObservableTest()
        {
            var observer = new TestObserver<float>();
            this.usecase.PausedTimeAsObservable.Subscribe(observer);
            Assert.AreEqual(0, observer.OnNextCount);

            this.usecase.Start();
            Assert.AreEqual(0, observer.OnNextCount);

            this.frameDiffTime.OnNext(1f);
            Assert.AreEqual(0, observer.OnNextCount);

            this.usecase.Pause();
            Assert.AreEqual(1, observer.OnNextCount);
            Assert.AreEqual(1f, observer.OnNextValues[0]);

            this.usecase.Resume();
            Assert.AreEqual(1, observer.OnNextCount);
            Assert.AreEqual(1f, observer.OnNextValues[0]);

            this.usecase.Stop();
            Assert.AreEqual(1, observer.OnNextCount);
            Assert.AreEqual(1f, observer.OnNextValues[0]);
        }

        [Test]
        public void ResumedAsObservableTest()
        {
            var observer = new TestObserver<Unit>();
            this.usecase.ResumedTimeAsObservable.Subscribe(observer);
            Assert.AreEqual(0, observer.OnNextCount);

            this.usecase.Start();
            Assert.AreEqual(0, observer.OnNextCount);

            this.usecase.Pause();
            Assert.AreEqual(0, observer.OnNextCount);

            this.usecase.Resume();
            Assert.AreEqual(1, observer.OnNextCount);

            this.usecase.Stop();
            Assert.AreEqual(1, observer.OnNextCount);
        }

        [Test]
        public void TimeAsObservableTest()
        {
            var observer = new TestObserver<float>();
            this.usecase.TimeAsObservable.Subscribe(observer);
            Assert.AreEqual(0, observer.OnNextCount);

            this.usecase.Start();
            Assert.AreEqual(1, observer.OnNextCount);
            Assert.AreEqual(0f, observer.OnNextLastValue);

            this.frameDiffTime.OnNext(1f);
            Assert.AreEqual(2, observer.OnNextCount);
            Assert.AreEqual(1f, observer.OnNextLastValue);

            this.usecase.Pause();
            this.frameDiffTime.OnNext(1f);
            Assert.AreEqual(2, observer.OnNextCount);
            Assert.AreEqual(1f, observer.OnNextLastValue);

            this.usecase.Resume();
            this.frameDiffTime.OnNext(1f);
            Assert.AreEqual(3, observer.OnNextCount);
            Assert.AreEqual(2f, observer.OnNextLastValue);

            this.usecase.Stop();
            this.frameDiffTime.OnNext(1f);
            Assert.AreEqual(3, observer.OnNextCount);
            Assert.AreEqual(2f, observer.OnNextLastValue);
        }

        [Test]
        public void IsPlayingTest()
        {
            var observer = new TestObserver<bool>();
            this.usecase.IsPlayingAsObservable.Subscribe(observer);
            Assert.AreEqual(0, observer.OnNextCount);
            Assert.IsFalse(this.usecase.IsPlaying);

            this.usecase.Start();
            Assert.IsTrue(observer.OnNextLastValue);
            Assert.IsTrue(this.usecase.IsPlaying);

            this.usecase.Pause();
            Assert.IsFalse(observer.OnNextLastValue);
            Assert.IsFalse(this.usecase.IsPlaying);

            this.usecase.Resume();
            Assert.IsTrue(observer.OnNextLastValue);
            Assert.IsTrue(this.usecase.IsPlaying);

            this.usecase.Stop();
            Assert.IsFalse(observer.OnNextLastValue);
            Assert.IsFalse(this.usecase.IsPlaying);
        }
    }
}

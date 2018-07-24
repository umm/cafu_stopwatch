using System;
using CAFU.Core.Presentation.View;
using CAFU.Stopwatch.Presentation.View;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Example.CAFU.Stopwatch.Presentation.View
{
    [RequireComponent(typeof(Text))]
    public class StoppedStopwatchText : MonoBehaviour, IView
    {
        public string Format = @"mm\:ss";

        void Start()
        {
            this.GetPresenter<IStopwatchPresenter>()
                .GetStoppedTimeAsObservable()
                .Select(it => Mathf.FloorToInt(it))
                .Select(it => TimeSpan.FromSeconds(it))
                .Subscribe(duration => this.GetComponent<Text>().text = duration.ToString(this.Format))
                .AddTo(this);
        }
    }
}

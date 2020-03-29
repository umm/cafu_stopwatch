using System;
using CAFU.Core.Presentation.View;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace CAFU.Stopwatch.Presentation.View
{
    [RequireComponent(typeof(Text))]
    public class StopwatchText : MonoBehaviour, IView
    {
        public string Format = @"mm\:ss";

        void Start()
        {
            this.GetPresenter<IStopwatchPresenter>().GetTimeAsObservable()
                .Select(it => Mathf.FloorToInt(it))
                .Select(it => TimeSpan.FromSeconds(it))
                .Subscribe(duration => this.GetComponent<Text>().text = duration.ToString(this.Format))
                .AddTo(this);
        }
    }
}
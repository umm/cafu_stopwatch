using CAFU.Core.Presentation.View;
using CAFU.Stopwatch.Presentation.View;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Example.CAFU.Stopwatch.Presentation.View
{
    [RequireComponent(typeof(Button))]
    public class ButtonStart : UIBehaviour, IView
    {
        protected override void Start()
        {
            base.Start();

            this.GetComponent<Button>()
                .OnClickAsObservable()
                .Subscribe(_ => this.GetPresenter<IStopwatchPresenter>().StartStopwatch())
                .AddTo(this);
        }
    }
}

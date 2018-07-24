# cafu\_stopwatch

## What

* stopwatch on cafu framework

## Requirement

* cafu\_core
* stopwatch

## Install

```shell
yarn add "umm-projects/cafu_stopwatch#^1.0.0"
```

## Usage

Implement IStopwatchPresenter on your presenter

- [Example code](https://github.com/umm/cafu_stopwatch/blob/master/Assets/Examples/Scripts/Presentation/Presenter/SamplePresenter.cs#L7)

```csharp
public class SamplePresenter : IStopwatchPresenter { /* implement */ }
```

Now easy to refer stopwatch event.

```
var presenter = this.GetPresenter<IStopwatchPresenter>();

// start
presenter.StartStopwatch();
// stop
presenter.StopStopwatch();
// resume
presenter.ResumeStopwatch();
// pause
presenter.PauseStopwatch();

// time observing
presenter.GetTimeAsObservable().Subscribe(time => this.Text.text = $"{time} seconds");

// stop observing
presenter.GetStoppedTimeAsObservable().Subscribe(time => this.Text.text = $"{time} seconds");
```

## License

Copyright (c) 2018 Takuma Maruyama

Released under the MIT license, see [LICENSE.txt](LICENSE.txt)


using System;
using System.Timers;
using UniRx;
using UnityEngine;
using Zenject;

public class FinalStageController : ControllerBase<FinalStageController, FinalStageViewModel>
{
    [Inject]
    private CompletionController next;
    [Inject]
    private GameController gameController;
    [Inject]
    private ScreenFlasherController screenFlasherController;

    private CompositeDisposable resetStage;

    private bool disableOutlinePending = false;
    private const float timerGranularity = 0.8f;

    Subject<int> onMouseUpTimeOut = new Subject<int>();

    public override void OnEnable()
    {
        viewModel.raycaster.enabled = true;

        resetStage = new CompositeDisposable();

        resetStage.Add(Disposable.Create(() => {
            viewModel.FillBar.fillAmount = 0;
            viewModel.Ball.isKinematic = true;

            disableOutlinePending = false;
        }));

        resetStage.Add(gameController.OnTimeElapsed.ObserveOnMainThread<TimeSpan>().
            Subscribe(this.OnTimeElapsed));

        resetStage.Add(Observable.Timer(TimeSpan.FromSeconds(0.5f)).Subscribe(_ =>
        {
            viewModel.Ball.isKinematic = false;
            viewModel.Ball.velocity = Vector3.zero;
            viewModel.Ball.AddForce(new Vector3(200, 0, 100), ForceMode.Impulse);
        }));

        resetStage.Add(onMouseUpTimeOut.ObserveOnMainThread<int>().Subscribe(this.OnMouseUpTimeOut));
    }

    public override void OnNext()
    {
        viewModel.raycaster.enabled = false;

        resetStage.Dispose();
        gameController.EndGame();

        screenFlasherController.Flash(this, next);
    }

    public void OnTimeElapsed(TimeSpan value)
    {
        viewModel.TimeElapsed.text = gameController.GameTime;
    }

    public void FillUpBar()
    {
        EnableOutline();

        viewModel.FillBar.fillAmount += 0.05f;
        if (viewModel.FillBar.fillAmount == 1.0f)
        {
            OnNext();
        }
    }

    public void EnableOutline()
    {
    }
    public void DisableOutline()
    {
        if (!resetStage.IsDisposed)
        {
            if (!disableOutlinePending)
            {
                disableOutlinePending = true;

                FireTimerForMouseUp();
            }
        }
    }

    private void FireTimerForMouseUp()
    {
        resetStage.Add(Observable.Timer(TimeSpan.FromSeconds(timerGranularity)).Subscribe(_ =>
        {
            OnMouseUpTimeOut(0);
        }));
    }
    private void FireTimerAsyncForMouseUp()
    {
        Timer timer = new Timer(TimeSpan.FromSeconds(timerGranularity).Milliseconds);
        resetStage.Add(timer);
        timer.Elapsed += (s, e) =>
        {
            onMouseUpTimeOut.OnNext(0);
        };
        timer.AutoReset = false;
        timer.Enabled = true;
    }

    private void OnMouseUpTimeOut(int noData)
    {
        disableOutlinePending = false;
    }
}
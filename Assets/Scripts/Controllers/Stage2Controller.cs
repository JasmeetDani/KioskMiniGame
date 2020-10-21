using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

public class Stage2Controller : ControllerBase<Stage2Controller, Stage2ViewModel>
{
    [Inject]
    private FinalStageController next;
    [Inject]
    private GameController gameController;
    private CompositeDisposable resetStage;
    [Inject]
    private DiContainer container;

    private Stage2ContentViewModel contenViewModel;
    private GameObject Content;

    private const float speed = 300.0f;
    private readonly Vector3 shellsTargetPos = new Vector3(0, 150, 100);
    public Queue<int> FlapsQ { get; } = new Queue<int>();

    public override void OnEnable()
    {
        resetStage = new CompositeDisposable();

        Content = container.InstantiatePrefab(viewModel.Content.gameObject, viewModel.Parent);
        contenViewModel = Content.GetComponent<Stage2ContentViewModel>();
        resetStage.Add(Disposable.Create(() => {
            UnityEngine.Object.Destroy(Content);

            FlapsQ.Clear();
        }));

        resetStage.Add(gameController.OnTimeElapsed.ObserveOnMainThread<TimeSpan>().Subscribe(this.OnTimeElapsed));
    }
    public override void OnDisable()
    {
        resetStage.Dispose();
    }

    public override void OnNext()
    {
        Disable();
        next.Enable();
    }

    public void OnTimeElapsed(TimeSpan value)
    {
        viewModel.TimeElapsed.text = gameController.GameTime;
    }

    public async void OnShellsCollide()
    {
        contenViewModel.DisableShellDrag();
        await contenViewModel.LerpShellsToFormBall(speed, shellsTargetPos);

        contenViewModel.Flaps.gameObject.SetActive(true);
        contenViewModel.RubberBall.gameObject.SetActive(true);
        contenViewModel.WrapsParent.gameObject.SetActive(true);
        contenViewModel.ShellsJoined = true;
    }

    public void ShellReadyToWrap(int index)
    {
        FlapsQ.Enqueue(index);
        contenViewModel.ShellsAnimator.SetTrigger("Next");
    }
}
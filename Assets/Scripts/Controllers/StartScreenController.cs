using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class StartScreenController : ControllerBase<StartScreenController, StartScreenViewModel>
{
    [Inject]
    private HomeController homeController;
    [Inject] 
    private LeaderboardController leaderboardController;
    [Inject]
    private DataCollectionController next;
    private const float timerGranularity = 3.0f;
    private CancellationTokenSource source = null;

    public override void OnNext()
    {
        Disable();
        next.Enable();
    }

    public override void OnEnable()
    {
        viewModel.SlideAnimator.enabled = true;
        viewModel.SlideAnimator.Rebind();

        homeController.Enable();
        leaderboardController.Enable();
    }

    public override void OnDisable()
    {
        source?.Cancel();
        source = null;

        viewModel.SlideAnimator.enabled = false;
        viewModel.Home.SetAsFirstSibling();
        RectTransform r = viewModel.Parent as RectTransform;
        r.anchorMin = Vector2.zero;
        r.anchorMax = new Vector2(2, 1);

        homeController.Disable();
        leaderboardController.Disable();
    }

    public async void SwapSubScreen()
    {
        viewModel.SlideAnimator.enabled = false;

        source = new CancellationTokenSource();
        try
        {
            await Task.Delay(TimeSpan.FromSeconds(timerGranularity), source.Token);
            viewModel.Parent.GetChild(0).SetAsLastSibling();
            viewModel.SlideAnimator.enabled = true;
        }
        catch(TaskCanceledException ex)
        {
        }
    }
}
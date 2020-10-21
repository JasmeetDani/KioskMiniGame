using System.Threading.Tasks;
using Zenject;

public class CompletionController : ControllerBase<CompletionController,CompletionViewModel>
{
    [Inject]
    private FinalLeaderboardController next;
    [Inject]
    private GameController gameController;
    [Inject]
    private DiContainer container;

    public async override void OnEnable()
    {
        viewModel.GUIRayCaster.enabled = false;
        viewModel.UI.gameObject.SetActive(false);
        viewModel.CreateDunlopBall(container);
        viewModel.FinishedBallContainer.gameObject.SetActive(true);

        await viewModel.WaitForDunopBallAnimationComplete();
        await Task.Delay(1000);

        viewModel.GUIRayCaster.enabled = true;
        viewModel.UI.gameObject.SetActive(true);
        viewModel.DestroyDunlopBall();
        viewModel.FinishedBallContainer.gameObject.SetActive(false);

        viewModel.GameTime = gameController.GameTime;
    }

    public override void OnNext()
    {
        Disable();
        next.Enable();
    }

    public void AnimationComplete()
    {
        viewModel.AnimationComplete();
    }
}
using UnityEngine;
using Zenject;

public class HomeController : ControllerBase<HomeController, HomeViewModel>
{
    [Inject]
    private StartScreenController startScreenController;

    public override void OnNext()
    {
        startScreenController.OnNext();
    }

    public override void OnEnable()
    {
        foreach (var ball in viewModel.balls)
        {
            ball.velocity = Vector3.zero;
            ball.AddForce(new Vector3(200, 0, 200), ForceMode.Impulse);
        }
    }
}
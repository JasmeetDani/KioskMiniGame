using Zenject;

public class ThankYouController : ControllerBase<ThankYouController, ThankYouViewModel>
{
    [Inject]
    private StartScreenController next;
    
    public override void OnNext()
    {
        Disable();

        next.Enable();
    }
}